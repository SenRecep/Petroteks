using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Areas.Admin.Models;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Services;

namespace Petroteks.MvcUi.Controllers
{
    public class HomeController : GlobalController
    {
        private readonly IMainPageService mainPageService;
        private readonly IAboutUsObjectService aboutUsObjectService;
        private readonly IPrivacyPolicyObjectService privacyPolicyObjectService;
        private readonly IBlogService blogService;
        private readonly ILanguageService languageService;
        private readonly ICategoryService categoryService;
        private readonly IDynamicPageService dynamicPageService;
        private readonly ILanguageCookieService languageCookieService;
        private readonly IProductService productService;
        private readonly IUI_ContactService uI_ContactService;
        private readonly IEmailService emailService;
        private readonly IRouteTable routeTable;

        private readonly UrlControlHelper urlControlHelper;

        public HomeController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            aboutUsObjectService = serviceProvider.GetService<IAboutUsObjectService>();
            privacyPolicyObjectService = serviceProvider.GetService<IPrivacyPolicyObjectService>();
            blogService = serviceProvider.GetService<IBlogService>();
            languageService = serviceProvider.GetService<ILanguageService>();
            categoryService = serviceProvider.GetService<ICategoryService>();
            dynamicPageService = serviceProvider.GetService<IDynamicPageService>();
            languageCookieService = serviceProvider.GetService<ILanguageCookieService>();
            productService = serviceProvider.GetService<IProductService>();
            emailService = serviceProvider.GetService<IEmailService>();
            uI_ContactService = serviceProvider.GetService<IUI_ContactService>();
            mainPageService = serviceProvider.GetService<IMainPageService>();
            urlControlHelper = serviceProvider.GetService<UrlControlHelper>();
            routeTable = serviceProvider.GetService<IRouteTable>();
        }

        [Route("")]
        public IActionResult Index()
        {
            MainPage mainPage;
            mainPage = mainPageService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            if (mainPage == null)
            {
                return PreparingPage();
            }

            return View(new MainPageViewModel()
            {
                MainPage = mainPage,
            });
        }
        [Route("Sayfalar/{pageName}-{id:int}")]
        public IActionResult DynamicPageView(int id)
        {
            DynamicPage dynamicPage = dynamicPageService.Get(x => x.WebSiteid == CurrentWebsite.id && x.IsActive == true && x.id == id, CurrentLanguage.id);
            if (dynamicPage == null)
            {
                return PreparingPage();
            }

            return View(dynamicPage);
        }
        [Route("{blogPageName}.html")]
        public IActionResult PetroBlog(string blogPageName)
        {
            if (routeTable.Exists(blogPageName, Bll.Concreate.EntityName.Blog, Bll.Concreate.PageType.List))
            {
                ICollection<Blog> blogs = blogService
               .GetMany(x => x.WebSiteid == CurrentWebsite.id && x.IsActive == true, CurrentLanguage.id)
               .OrderByDescending(x => x.Priority)
               .ThenBy(x => x.CreateDate).ToList();
                return View(blogs);
            }
            return NotFoundPage();
        }


        [Route("sondaj-kopugu-nedir.html")]
        public IActionResult SondajKopugu()
        {
            return View();
        }
        
        [Route("Blog-Detay/3/sondaj-kimyasallari")]
        public IActionResult Sondajkim()
        {
            return View();
        }
        [Route("Hakimizda")]
        public IActionResult AboutUs()
        {
            AboutUsObject aboutUsObject;
            aboutUsObject = aboutUsObjectService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            if (aboutUsObject == null)
            {
                return PreparingPage();
            }

            return View(aboutUsObject);
        }
        [Route("Iletisim")]
        public IActionResult Contact()
        {
            UI_Contact uI_Contact = uI_ContactService.Get(x => x.IsActive == true && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            if (uI_Contact == null)
            {
                return PreparingPage();
            }

            return View(uI_Contact);
        }
        [Route("Gizlilik-Politikasi")]
        public IActionResult PrivacyPolicy()
        {
            PrivacyPolicyObject gizlilikpolitikasi;
            gizlilikpolitikasi = privacyPolicyObjectService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            if (gizlilikpolitikasi == null)
            {
                return PreparingPage();
            }

            return View(gizlilikpolitikasi);
        }

        [HttpPost]
        public JsonResult Contact(Iletisim model)
        {
            if (ModelState.IsValid)
            {
                StringBuilder body = new StringBuilder();
                body.AppendLine("İsim:" + model.İsim);
                body.AppendLine("Konu:" + model.Konu);
                body.AppendLine("Mesaj:" + model.Mesaj);
                body.AppendLine("Eposta:" + model.Email);
                Mail.SendMail(body.ToString());
                return Json("Başarılı bir şekilde iletildi");
            }
            return Json("Hata");
        }

        public JsonResult BilgilendirmeMail(string email)
        {
            Email Email;
            Email = emailService.Get(x => x.EmailAddress.Equals(email) && x.WebSiteid == CurrentWebsite.id);
            if (Email == null)
            {
                bool IsValid = RegexUtilities.IsValidEmail(email);
                if (IsValid)
                {
                    emailService.Add(new Email() { EmailAddress = email, WebSite = CurrentWebsite });
                    emailService.Save();
                    return Json("Başarılı bi şekilde abone oldunuz.");
                }
                return Json("Lütfen Mail Adresinizi Doğru bir Şekilde Giriniz");
            }
            return Json("Zaten Abonesiniz.");
        }


        [Route("Language-Change/language-{KeyCode}")]
        public IActionResult ChangeCulture(string KeyCode)
        {
            if (!string.IsNullOrWhiteSpace(KeyCode))
            {
                Language language = languageService.Get(x => x.KeyCode.Equals(KeyCode) && x.WebSiteid == CurrentWebsite.id && x.IsActive == true);
                if (language != null)
                {
                    SetLanguage(language);
                }
            }


            return RedirectToAction("Index", "Home");
        }


    }
}
