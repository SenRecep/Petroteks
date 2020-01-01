using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Concreate;
using Petroteks.Bll.Helpers;
using Petroteks.Core.Dal;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Models;
using System;
using System.Collections.Generic;

namespace Petroteks.MvcUi.Controllers
{
    public class HomeController : GlobalController
    {
        private readonly IMainPageService mainPageService;
        private readonly IAboutUsObjectService aboutUsObjectService;
        private readonly IPrivacyPolicyObjectService privacyPolicyObjectService;
        private readonly IEmailService emailService;

        public HomeController(IAboutUsObjectService aboutUsObjectService, IMainPageService mainPageService, IEmailService emailService, IPrivacyPolicyObjectService privacyPolicyObjectService, IWebsiteService websiteService, IHttpContextAccessor httpContextAccessor) : base(websiteService, httpContextAccessor)
        {
            this.aboutUsObjectService = aboutUsObjectService;
            this.mainPageService = mainPageService;
            this.privacyPolicyObjectService = privacyPolicyObjectService;
            this.emailService = emailService;
        }
        //url/Home/Index
        public IActionResult Index()
        {
            MainPage mainPage; 
            mainPage = mainPageService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (mainPage==null)
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            return View(mainPage);
        }
         
        public IActionResult Hakkimizda()
        {
            AboutUsObject hakkimizda;
            hakkimizda = aboutUsObjectService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (hakkimizda == null)
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            return View(hakkimizda);
        }

        public IActionResult GizlilikPolitikasi()
        {
            PrivacyPolicyObject gizlilikpolitikasi;
            gizlilikpolitikasi = privacyPolicyObjectService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (gizlilikpolitikasi == null)
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            return View(gizlilikpolitikasi);
        }


        public JsonResult BilgilendirmeMail(string email)
        {
            Email Email;
            Email = emailService.Get(x=>x.EmailAddress.Equals(email) && x.WebSiteid==ThisWebsite.id);
            if (Email==null)
            {
                bool IsValid = RegexUtilities.IsValidEmail(email);
                if (IsValid)
                {
                    emailService.Add(new Email() { EmailAddress=email,WebSite=ThisWebsite});
                    emailService.Save();
                    return Json("Başarılı bi şekilde abone oldunuz.");
                }
                return Json("Lütfen Mail Adresinizi Doğru bir Şekilde Giriniz");
            }
            return Json("Zaten Abonesiniz.");
        }


    }
}
