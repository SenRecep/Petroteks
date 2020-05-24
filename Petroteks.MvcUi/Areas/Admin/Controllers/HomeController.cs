using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Areas.Admin.Data;
using Petroteks.MvcUi.Areas.Admin.Models;
using Petroteks.MvcUi.Attributes;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Petroteks.MvcUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : AdminBaseController
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;
        private readonly IWebsiteService websiteService;
        private readonly ILanguageService languageService;
        private readonly ILanguageCookieService languageCookieService;
        private readonly IEmailService emailService;
        private readonly IUI_NoticeService uI_NoticeService;
        private EmailSender emailSender;
        public HomeController(
            IUserService userService,
            IUserSessionService userSessionService,
            IWebsiteService websiteService,
            ILanguageService languageService,
            IHttpContextAccessor httpContextAccessor,
            ILanguageCookieService languageCookieService,
            IEmailService emailService,
            IUI_NoticeService uI_NoticeService) :
            base(userSessionService, websiteService, languageService, languageCookieService, httpContextAccessor)
        {
            _userService = userService;
            _userSessionService = userSessionService;
            this.websiteService = websiteService;
            this.languageService = languageService;
            this.languageCookieService = languageCookieService;
            this.emailService = emailService;
            this.uI_NoticeService = uI_NoticeService;
        }
        [Route("Admin-Panel")]
        [AdminAuthorize]
        public IActionResult Index()
        {
            ViewBag.LoginUser = ViewBag.LoginUser;
            ICollection<User> allUsers = _userService.GetMany(x => x.IsActive == true);
            return View(allUsers);
        }
        [AllowAnonymous]
        [Route("Giris")]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous, HttpPost]
        [Route("Giris")]
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

                        return RedirectToAction("Index", "Home", new { area = "Admin" });
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
                    TempData["message2"] = "Lütfen hesabınızın onaylanmasını bekleyin.";
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

        [AllowAnonymous]
        [Route("Kayit")]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous, HttpPost]
        [Route("Kayit")]
        public IActionResult Register(RegisterViewModel model)
        {

            TempData["errorMessage"] = "";
            if (ModelState.IsValid)
            {
                if (model.Password != model.PasswordConfirmation)
                {
                    TempData["errorMessage"] += "Şifreler birbiriyle uyuşmamaktadır.\n";
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
            return RedirectToAction("Login", "Home", new { area = "Admin" });
        }
        [AdminAuthorize]
        [Route("DuyuruList")]
        public IActionResult DuyuruList()
        {
            List<UI_Notice> data = uI_NoticeService.GetMany(x => x.WebSiteid == WebsiteContext.CurrentWebsite.id && x.IsActive == true).ToList();
            return View(data);
        }
        [AdminAuthorize]
        [Route("Duyuru")]
        public IActionResult Duyuru()
        {
            return View(new NoticeViewModel());
        }
        [AdminAuthorize]
        [Route("Duyuru-Silme-{id:int}")]
        public JsonResult DuyuruSilme(int id)
        {
            UI_Notice Notice = uI_NoticeService.Get(x => x.id == id);
            if (Notice != null)
            {
                Notice.IsActive = false;
                uI_NoticeService.Save();
                return Json("Başarılı");
            }
            return Json("Basarisiz");
        }
        [HttpPost]
        [AdminAuthorize]
        [Route("Duyuru")]
        public IActionResult Duyuru(NoticeViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool error = false;
                StringBuilder ErrorMassage = new StringBuilder();
                if (model.StartDate > model.EndDate)
                {
                    ErrorMassage.AppendLine("Baslangic tarihiniz bitis tarihinden sonra olamaz.");
                    error = true;
                }
                if (string.IsNullOrWhiteSpace(model.Content))
                {
                    ErrorMassage.AppendLine("Duyuru icerigi bos olamaz");
                    error = true;
                }
                if (!error)
                {
                    UI_Notice uI_Notice = new UI_Notice()
                    {
                        Content = model.Content,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        WebSite = WebsiteContext.CurrentWebsite,
                        Language = LanguageContext.CurrentLanguage,
                        CreateUserid = LoginUser.id
                    };
                    uI_NoticeService.Add(uI_Notice);
                    uI_NoticeService.Save();
                }
                else
                {
                    ViewBag.CreateError = ErrorMassage.ToString();
                    return View(model);
                }
            }
            return View(new NoticeViewModel());
        }


        [AdminAuthorize]
        [Route("Bilgilendirme")]
        public IActionResult Bilgilendirme()
        {
            emailSender = new EmailSender(emailService);
            ICollection<Email> emails = emailSender.LoadWebsiteEmails(Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
            MailViewModel model = new MailViewModel()
            {
                Emails = emails,
                Body = emailSender.Body
            };
            return View(model);
        }

        [HttpPost]
        [AdminAuthorize]
        [Route("Bilgilendirme")]
        public IActionResult Bilgilendirme(MailViewModel model)
        {
            if (emailSender == null)
            {
                emailSender = new EmailSender(emailService);
            }

            emailSender.Body = model.Body;
            emailSender.Subject = model.Subject;
            Attachment file = null;
            if (model.File != null)
            {
                string fileName = Path.GetFileName(model.File.FileName);
                file = new Attachment(model.File.OpenReadStream(), fileName);
            }

            if (emailSender.Send(DataTransferPoint.SelectedEmails, file))
            {
                return RedirectToAction("Bilgilendirme", "Home", new { area = "Admin" });
            }
            return View(new MailViewModel()
            {
                Emails = emailSender.LoadWebsiteEmails(Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id)
            });
        }


        [HttpGet]
        [AdminAuthorize]
        [Route("SitelerinDurumlari")]
        public IActionResult WebsitesStatus()
        {
            return View();
        }


        [AdminAuthorize]
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
        [AdminAuthorize]
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

        [AdminAuthorize]
        public JsonResult BilgilendirmeEkle(string mail, string category)
        {
            try
            {
                emailSender = new EmailSender(emailService);
                if (emailSender.EmailAdd(Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite, mail, category) == true)
                {
                    return Json("Islem Basari ile tamamlandi");
                }
                else
                {
                    return Json("Üzgünüz Ters Giden Birşeyler var");
                }

            }
            catch
            {
                return Json("İşlem tamamlanamadı");
            }
        }
        [AdminAuthorize]
        public JsonResult BilgilendirmeSil(int id)
        {
            Email email = emailService.Get(x => x.id == id);
            if (email != null)
            {
                emailService.Delete(email);
                emailService.Save();
                return Json("Başarılı");
            }
            return Json("Basarisiz");
        }
        [AdminAuthorize]
        public JsonResult BilgilendirmeEmails(string json)
        {
            DataTransferPoint.SelectedEmails = new List<Email>();
            if (!string.IsNullOrWhiteSpace(json))
            {
                DataTransferPoint.SelectedEmails = JsonConvert.DeserializeObject<ICollection<Email>>(json);
            }

            return Json(true);
        }

        [Route("Admin/Language-Change/language-{KeyCode}")]
        public IActionResult ChangeCulture(string KeyCode)
        {
            if (!string.IsNullOrWhiteSpace(KeyCode))
            {
                Language language = languageService.Get(x => x.KeyCode.Equals(KeyCode) && x.WebSiteid == WebsiteContext.CurrentWebsite.id && x.IsActive == true);
                if (language != null)
                {
                    LanguageContext.CurrentLanguage = language;
                    languageCookieService.Set("CurrentLanguage", language, 60 * 24 * 7);
                }
            }
            return RedirectToAction("Index", "Home");
        }


        [Route("Admin/Website-Change/Website-{Name}")]
        public IActionResult ChangeWebsite(string Name)
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                Website website = websiteService.findByUrl(Name);
                if (website != null)
                {
                    WebsiteContext.CurrentWebsite = website;
                    LoadLanguage(true);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}