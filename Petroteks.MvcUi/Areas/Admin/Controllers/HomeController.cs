﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.Concrete;
using Petroteks.MvcUi.Areas.Admin.Models;
using Petroteks.MvcUi.Attributes;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Services;

namespace Petroteks.MvcUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public HomeController(IUserService userService, IUserSessionService userSessionService)
        {
            this._userService = userService;
            this._userSessionService = userSessionService;
        }
        [AdminAuthorize]
        public IActionResult Index()
        {
            ICollection<User> allUsers = _userService.GetMany(x => x.IsActive == true);
            ViewBag.LoginUser = _userSessionService.Get("LoginAdmin");
            ViewBag.PageViewModel = new PageViewModel();
            return View(allUsers);
        }

        [AdminAuthorize]
        [HttpPost]
        public IActionResult Index(PageViewModel model)
        {
            return View(model);
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
                        _userSessionService.Set(res, "LoginAdmin");
                        TempData["message"] = "Başarılı";
                        TempData["message2"] = "Giriş başarılı.";
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


    }
}