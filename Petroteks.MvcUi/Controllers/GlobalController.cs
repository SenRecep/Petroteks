using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Services;
using System.Linq;

namespace Petroteks.MvcUi.Controllers
{
    public class GlobalController : Controller
    {
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

            if (WebsiteContext.CurrentWebsite == null)
            {
                string url = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
                string siteName = httpContextAccessor.HttpContext.Request.Host.Value.Replace("www.", "", System.StringComparison.InvariantCultureIgnoreCase);
                WebsiteContext.Websites = websiteService.GetMany(x => x.IsActive == true);
                Website website = WebsiteContext.Websites.FirstOrDefault(x => x.Name.Equals(siteName, System.StringComparison.InvariantCultureIgnoreCase));
                if (website != null)
                {
                    WebsiteContext.CurrentWebsite = website;
                }
                else
                {
                    Website wb = new Website()
                    {
                        BaseUrl = url,
                        Name = siteName
                    };
                    websiteService.Add(wb);
                    websiteService.Save();
                    WebsiteContext.CurrentWebsite = wb;
                    WebsiteContext.Websites = websiteService.GetMany(x => x.IsActive == true);
                }
                if (!WebsiteContext.CurrentWebsite.Name.Contains("localhost"))
                {
                    Website localhost = WebsiteContext.Websites.FirstOrDefault(w => w.Name.Contains("localhost"));
                    WebsiteContext.Websites.Remove(localhost);
                }
                LoadLanguage();
            }
        }
        public void LoadLanguage(bool decision = false, int? id = null)
        {
            LanguageContext.WebsiteLanguages = languageService.GetMany(x => x.IsActive == true && x.WebSiteid == WebsiteContext.CurrentWebsite.id);
            Language currentLanguage = languageCookieService.Get("CurrentLanguage");
            if (decision)
            {
                currentLanguage = null;
            }

            if (decision && id != null)
            {
                currentLanguage = LanguageContext.WebsiteLanguages.FirstOrDefault(x => x.id == id);
                languageCookieService.Set("CurrentLanguage", currentLanguage, 60 * 24 * 7);
            }
            if (currentLanguage == null)
            {
                Language dbcurrentLanguage = LanguageContext.WebsiteLanguages.FirstOrDefault(x => x.Default == true);
                if (dbcurrentLanguage == null)
                {
                    dbcurrentLanguage = new Language()
                    {
                        Default = true,
                        KeyCode = "tr-TR",
                        Name = "Türkçe",
                        WebSite = WebsiteContext.CurrentWebsite,
                        IconCode = "tr-TR_Türkçe.png"
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