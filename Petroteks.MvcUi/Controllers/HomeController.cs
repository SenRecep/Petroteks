using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Areas.Admin.Models;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Services;
using Petroteks.MvcUi.StringInfos;

namespace Petroteks.MvcUi.Controllers
{
    public class HomeController : GlobalController
    {
        private readonly IMainPageService mainPageService;
        private readonly IAboutUsObjectService aboutUsObjectService;
        private readonly IPrivacyPolicyObjectService privacyPolicyObjectService;
        private readonly IBlogService blogService;
        private readonly ILanguageService languageService;
        private readonly IDynamicPageService dynamicPageService;
        private readonly IUI_ContactService uI_ContactService;
        private readonly IEmailService emailService;
        private readonly IRouteTable routeTable;
        private readonly ICacheService cacheService;


        public HomeController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            aboutUsObjectService = serviceProvider.GetService<IAboutUsObjectService>();
            privacyPolicyObjectService = serviceProvider.GetService<IPrivacyPolicyObjectService>();
            blogService = serviceProvider.GetService<IBlogService>();
            languageService = serviceProvider.GetService<ILanguageService>();
            dynamicPageService = serviceProvider.GetService<IDynamicPageService>();
            emailService = serviceProvider.GetService<IEmailService>();
            uI_ContactService = serviceProvider.GetService<IUI_ContactService>();
            mainPageService = serviceProvider.GetService<IMainPageService>();
            routeTable = serviceProvider.GetService<IRouteTable>();
            cacheService = serviceProvider.GetService<ICacheService>();
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            MainPage mainPage = await cacheService.GetAsync($"{CacheInfo.MainPage}-{CurrentWebsite.id}-{CurrentLanguage.id}", () =>
               mainPageService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id)
           );

            if (mainPage == null) return PreparingPage();

            return View(new MainPageViewModel()
            {
                MainPage = mainPage,
            });
        }
        [Route("Sayfalar/{pageName}-{id:int}")]
        public async Task<IActionResult> DynamicPageView(int id)
        {
            DynamicPage dynamicPage = await cacheService.GetAsync($"{CacheInfo.DynamicPage}-{id}-{CurrentWebsite.id}-{CurrentLanguage.id}", () =>
               dynamicPageService.Get(x => x.WebSiteid == CurrentWebsite.id && x.IsActive == true && x.id == id, CurrentLanguage.id)
           );
            if (dynamicPage == null)
            {
                return PreparingPage();
            }

            return View(dynamicPage);
        }


        [Route("{blogPageName}.html")]
        public async Task<IActionResult> PetroBlog(string blogPageName)
        {
            if (routeTable.Exists(blogPageName, Bll.Concreate.EntityName.Blog, Bll.Concreate.PageType.List))
            {
                ICollection<Blog> blogs = await cacheService.GetAsync($"{CacheInfo.OrderedBlogs}-{CurrentWebsite.id}-{CurrentLanguage.id}", () =>
                 blogService
                   .GetMany(x => x.WebSiteid == CurrentWebsite.id && x.IsActive == true, CurrentLanguage.id)
                   .OrderByDescending(x => x.Priority)
                   .ThenBy(x => x.CreateDate).ToList()
                );
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
        public async Task<IActionResult> AboutUs()
        {
            AboutUsObject aboutUsObject = await cacheService.GetAsync($"{CacheInfo.AboutUs}-{CurrentWebsite.id}-{CurrentLanguage.id}", () =>
             aboutUsObjectService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id)
         );
            if (aboutUsObject == null)
                return PreparingPage();

            return View(aboutUsObject);
        }
        [Route("Iletisim")]
        public async Task<IActionResult> Contact()
        {
            UI_Contact uI_Contact = await cacheService.GetAsync($"{CacheInfo.Contact}-{CurrentWebsite.id}-{CurrentLanguage.id}", () =>
                uI_ContactService.Get(x => x.IsActive == true && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id)
        );
            if (uI_Contact == null)
            {
                return PreparingPage();
            }

            return View(uI_Contact);
        }
        [Route("Gizlilik-Politikasi")]
        public async Task<IActionResult> PrivacyPolicy()
        {
            PrivacyPolicyObject privacyPolicy = await cacheService.GetAsync($"{CacheInfo.PrivacyPolicy}-{CurrentWebsite.id}-{CurrentLanguage.id}", () =>
                privacyPolicyObjectService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id)
        );
            if (privacyPolicy == null)
            {
                return PreparingPage();
            }

            return View(privacyPolicy);
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
