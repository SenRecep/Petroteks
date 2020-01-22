using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Concreate;
using Petroteks.Bll.Helpers;
using Petroteks.Core.Dal;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Petroteks.MvcUi.Areas.Admin.Models;

namespace Petroteks.MvcUi.Controllers
{
    public class HomeController : PublicController
    {
        private readonly IMainPageService mainPageService;
        private readonly IAboutUsObjectService aboutUsObjectService;
        private readonly IPrivacyPolicyObjectService privacyPolicyObjectService;
        private readonly IBlogService blogService;
        private readonly IDynamicPageService dynamicPageService;
        private readonly IEmailService emailService;

        public HomeController(
            IAboutUsObjectService aboutUsObjectService,
            IMainPageService mainPageService,
            IEmailService emailService,
            IPrivacyPolicyObjectService privacyPolicyObjectService,
            IWebsiteService websiteService,
            IHttpContextAccessor httpContextAccessor,
            IBlogService blogService,
            IDynamicPageService dynamicPageService) :
            base(websiteService, httpContextAccessor)
        {
            this.aboutUsObjectService = aboutUsObjectService;
            this.mainPageService = mainPageService;
            this.privacyPolicyObjectService = privacyPolicyObjectService;
            this.blogService = blogService;
            this.dynamicPageService = dynamicPageService;
            this.emailService = emailService;
        }


        //url/Home/Index
        public IActionResult Index()
        {
            ICollection<Blog> blogs = blogService.GetMany(x=>x.WebSiteid==ThisWebsite.id && x.IsActive==true).OrderByDescending(x=>x.CreateDate).Take(3).ToList();
            MainPage mainPage; 
            mainPage = mainPageService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (mainPage==null)
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            return View(new MainPageViewModel() { 
                MainPage= mainPage,
                Blogs=blogs
            });
        }

        public IActionResult DynamicPageView(int id)
        {
            DynamicPage dynamicPage = dynamicPageService.Get(x=>x.WebSiteid==ThisWebsite.id && x.IsActive==true && x.id==id);
            return View(dynamicPage);
        }

        public IActionResult PetroBlog()
        {
            ICollection<Blog> blogs = blogService.GetMany(x => x.WebSiteid == ThisWebsite.id && x.IsActive == true).OrderByDescending(x => x.CreateDate).ToList();
            return View(blogs);
        }
       
        public IActionResult BlogDetay(int id)
        {
            var findedBlog = blogService.Get(m => m.id == id);
            if (findedBlog != null)
            {
                return View(findedBlog);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Hakkimizda()
        {
            AboutUsObject hakkimizda;
            hakkimizda = aboutUsObjectService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (hakkimizda == null)
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            return View(hakkimizda);
        } 
        public IActionResult Iletisim()
        { 
            return View();
        }
        [HttpPost]
        public JsonResult Iletisim(Iletisim model)
        {

            if (ModelState.IsValid)
            {
                var body = new StringBuilder();
                body.AppendLine("İsim:" + model.İsim);
                body.AppendLine("Konu:" + model.Konu);
                body.AppendLine("Mesaj:" + model.Mesaj);
                body.AppendLine("Eposta:" + model.Email);
                Mail.SendMail(body.ToString()); 
                return Json("Başarılı bir şekilde iletildi");
            } 
            return Json("Hata");
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
