using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Areas.Admin.Models;
using Petroteks.MvcUi.Attributes;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Services;

namespace Petroteks.MvcUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : AdminBaseController
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;
        private readonly IEmailService emailService;
        private EmailSender emailSender;
        public HomeController(
            IUserService userService,
            IUserSessionService userSessionService,
            IWebsiteService websiteService,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService) :
            base(userSessionService, websiteService, httpContextAccessor)
        {
            this._userService = userService;
            this._userSessionService = userSessionService;
            this.emailService = emailService;
        }
        [AdminAuthorize]
        public IActionResult Index()
        {
            ViewBag.LoginUser = ViewBag.LoginUser;
            ICollection<User> allUsers = _userService.GetMany(x => x.IsActive == true);
            return View(allUsers);
        }


        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous, HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Password) && _userService.GetMany(user => user.Email.Equals(model.Email) && user.Role != 2).Count > 0)
            {
                model.Password = ToPasswordRepository.PasswordCryptographyCombine(model.Password);
                User res = _userService.GetMany(x => x.Email == model.Email && x.Role != 2).First();
                if (res.Password != model.Password)
                {
                    TempData["message"] = "Hata";
                    TempData["message2"] = "Girdiğiniz parolalar eşleşmiyor.";
                    goto Finish;
                }

                if (res.IsActive == true)
                {
                    if (res.Role != 2)
                    {
                        res.UpdateDate = DateTime.UtcNow;
                        res.UpdateUserid = res.id;
                        _userSessionService.Set(res, "LoginAdmin");
                        TempData["message"] = "Başarılı";
                        TempData["message2"] = "Giriş başarılı.";
                        _userService.Update(res);
                        _userService.Save();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["message"] = "Hata";
                        TempData["message2"] = "Yetersiz Yetki";
                    }
                }
                else
                {
                    TempData["message"] = "Hata";
                    TempData["message2"] = "Lütfen doğrulama e-postası ile hesabınızı doğrulayın.";
                    goto Finish;
                }
            }
            else
            {
                TempData["message"] = "Hata";
                TempData["message2"] = "Kullanıcı adı veya şifre hatalı.";
                goto Finish;
            }




        Finish:

            return View();
        }

        public JsonResult SelectAuth(int id, int role)
        {
            User user = _userService.Get(x => x.id == id);
            if (user != null)
            {
                user.Role = (short)role;
                user.UpdateDate = DateTime.UtcNow;
                user.UpdateUserid = LoginUser.id;
                _userService.Update(user);
                try
                {
                    _userService.Save();
                    return Json("Islem Basari ile tamamlandi");
                }
                catch
                {
                    return Json("İşlem tamamlanamadı");
                }

            }
            return new JsonResult("İşlem tamamlanamadı");
        }

        public JsonResult DeleteUser(int id)
        {
            User user = _userService.Get(x => x.id == id);
            if (user != null)
            {
                user.UpdateDate = DateTime.UtcNow;
                user.UpdateUserid = LoginUser.id;
                _userService.Delete(user);
                try
                {
                    _userService.Save();
                    return Json("Islem Basari ile tamamlandi");
                }
                catch
                {
                    return Json("İşlem tamamlanamadı");
                }

            }
            return new JsonResult("İşlem tamamlanamadı");
        }


        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous, HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {

            TempData["errorMessage"] = "";
            if (ModelState.IsValid)
            {
                if (model.Password != model.PasswordConfirmation)
                {
                    TempData["errorMessage"] += "Bu mail adresi daha önceden kayıtlıdır.\n";
                }
                if (_userService.GetMany(user => user.Email.Equals(model.Email) && user.Role != 2).Count > 0)
                {

                    TempData["errorMassage"] += "Bu mail adresi daha önceden kayıtlıdır.";
                }
                else
                {
                    model.Password = ToPasswordRepository.PasswordCryptographyCombine(model.Password);

                    User user = new User()
                    {
                        Firstname = model.FirstName,
                        Lastname = model.LastName,
                        Password = model.Password,
                        Email = model.Email,
                        TagName = model.UserName,
                        Role = 2,
                    };

                    try
                    {
                        _userService.Add(user);
                        _userService.Save();
                    }
                    catch
                    {
                        TempData["errorMassage"] += "Veri tabanına eklerken hata ile karşılaşıldı.";
                    }

                }
            }
            else
            {
                TempData["errorMassage"] = "Tüm değerleri doğru biçimde giriniz";
            }
            return RedirectToAction("Login", "Home");
        }



        [AdminAuthorize]
        public IActionResult Bilgilendirme()
        {
            emailSender = new EmailSender(emailService);
            ICollection<Email> emails = emailSender.LoadWebsiteEmails(ThisWebsite.id);
            MailViewModel model = new MailViewModel()
            {
                Emails = emails
            };
            return View(model);
        }

        [AdminAuthorize]
        [HttpPost]
        public IActionResult Bilgilendirme(MailViewModel model)
        {
            if (emailSender == null)
                emailSender = new EmailSender(emailService);
            emailSender.Body = model.Body;
            emailSender.Subject = model.Subject;
            Attachment file=null;
            if (model.File != null)
            {
                string fileName = Path.GetFileName(model.File.FileName);
                file = new Attachment(model.File.OpenReadStream(), fileName);
            }

            if (emailSender.Send(emailSender.LoadWebsiteEmails(ThisWebsite.id), file))
            {
                return RedirectToAction("Bilgilendirme", "Home", new { area = "Admin" });
            }
            return View(model);
        }

    }
}