using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Services;
using System.Globalization;
using System.Threading;

namespace Petroteks.MvcUi.Controllers
{
    public class GlobalController : Controller
    {
        public Website ThisWebsite { get; set; }
        private readonly IWebsiteService websiteService;
        private readonly ILanguageService languageService;
        private readonly ILanguageCookieService languageCookieService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public GlobalController(IWebsiteService websiteService, ILanguageService languageService, ILanguageCookieService languageCookieService, IHttpContextAccessor httpContextAccessor)
        {
            this.websiteService = websiteService;
            this.languageService = languageService;
            this.languageCookieService = languageCookieService;
            this.httpContextAccessor = httpContextAccessor;

            if (ThisWebsite == null)
            {
                string url = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
                string siteName = httpContextAccessor.HttpContext.Request.Host.Value.Replace("www.", "", System.StringComparison.InvariantCultureIgnoreCase);
                Website website = websiteService.findByUrl(siteName);
                if (website != null)
                    ThisWebsite = website;
                else
                {
                    Website wb = new Website()
                    {
                        BaseUrl = url,
                        Name = siteName
                    };
                    websiteService.Add(wb);
                    websiteService.Save();
                    ThisWebsite = wb;
                }
                LoadLanguage();
            }

        }
        public void LoadLanguage()
        {

            Language currentLanguage = languageCookieService.Get("CurrentLanguage");

            if (currentLanguage == null)
            {
                var culture = CultureInfo.CurrentCulture;

                Language dbcurrentLanguage = languageService.Get(x => x.IsActive == true && x.WebSiteid == ThisWebsite.id && x.KeyCode.Equals(culture.Name));
                if (dbcurrentLanguage == null)
                    dbcurrentLanguage = languageService.Get(x => x.IsActive == true && x.WebSiteid == ThisWebsite.id && x.Default == true);
                if (dbcurrentLanguage == null)
                {
                    dbcurrentLanguage = new Language()
                    {
                        Default = true,
                        KeyCode = "tr-TR",
                        Name = "Türkçe",
                        WebSite = ThisWebsite,
                        IconCode="tr.png"
                    };
                    languageService.Add(dbcurrentLanguage);
                    languageService.Save();
                }
                LanguageContext.CurrentLanguage = dbcurrentLanguage;
                languageCookieService.Set("CurrentLanguage", dbcurrentLanguage, 60 * 24 * 7);
            }
            else
            {
                LanguageContext.CurrentLanguage = currentLanguage;
            }
        }
    }
}