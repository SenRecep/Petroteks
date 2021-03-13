using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Services;
using Petroteks.MvcUi.StringInfos;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.Controllers
{
    public class GlobalController : Controller
    {
        private readonly IWebsiteService websiteService;
        private readonly ILanguageService languageService;
        private readonly ILanguageCookieService languageCookieService;
        private readonly IWebsiteCookieService websiteCookieService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICacheService cacheService;

        private readonly Language defaultLanguage;


        public GlobalController(IServiceProvider serviceProvider)
        {
            websiteService = serviceProvider.GetService<IWebsiteService>();
            languageService = serviceProvider.GetService<ILanguageService>();
            languageCookieService = serviceProvider.GetService<ILanguageCookieService>();
            httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            websiteCookieService = serviceProvider.GetService<IWebsiteCookieService>();
            cacheService = serviceProvider.GetService<ICacheService>();
            defaultLanguage = serviceProvider.GetService<IOptions<Language>>().Value;
            LoadWebsite();
        }


        public Language CurrentLanguage => languageCookieService.Get("CurrentLanguage");

        public void SetLanguage(Language language)
        {
            languageCookieService.Set("CurrentLanguage", language, int.MaxValue);
        }

        public int CurrentLanguageId => CurrentLanguage.id;

        public Website CurrentWebsite => websiteCookieService.Get("CurrentWebsite");
        public void SetWebsite(Website website)
        {
            websiteCookieService.Set("CurrentWebsite", website, int.MaxValue);
        }

        public int CurrentWebsiteId => CurrentWebsite.id;

        private void LoadWebsite()
        {
            if (!cacheService.Get($"{CacheInfo.Websites}") || WebsiteContext.Websites==null || CurrentWebsite == null)
            {
                WebsiteContext.Websites = cacheService.Get($"{CacheInfo.Websites}", () => websiteService.GetMany(x => x.IsActive == true));

                if (CurrentWebsite == null)
                {
                    string siteName = httpContextAccessor.HttpContext.Request.Host.Value.Replace("www.", "", StringComparison.InvariantCultureIgnoreCase);
                    Website website = WebsiteContext.Websites.FirstOrDefault(x => x.Name.Equals(siteName, StringComparison.InvariantCultureIgnoreCase));
                    if (website != null)
                    {
                        SetWebsite(website);
                    }
                    else
                    {
                        string url = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
                        CreateDefaultWebsite(url, siteName);
                        WebsiteContext.Websites = cacheService.UpdateGet($"{CacheInfo.Websites}", () => websiteService.GetMany(x => x.IsActive == true));
                    }

                    if (!CurrentWebsite.Name.Contains("localhost"))
                    {
                        var localhosts = WebsiteContext.Websites.Where(w => w.Name.Contains("localhost"));
                        foreach (var item in localhosts)
                            WebsiteContext.Websites.Remove(item);
                        WebsiteContext.Websites = cacheService.UpdateGet($"{CacheInfo.Websites}", () => websiteService.GetMany(x => x.IsActive == true));

                    }
                }
            }
            LoadLanguage();
        }

        private void CreateDefaultWebsite(string url, string siteName)
        {
            Website wb = new Website()
            {
                BaseUrl = url,
                Name = siteName
            };
            websiteService.Add(wb);
            websiteService.Save();
            SetWebsite(wb);
        }
        public void LoadLanguage(bool decision = false, int? id = null)
        {
            if (LanguageContext.WebsiteLanguages == null || !cacheService.Get($"{CacheInfo.Languages}")  ||  decision || CurrentLanguage == null)
            {
                LanguageContext.WebsiteLanguages = cacheService.Get($"{CacheInfo.Languages}", () => languageService.GetMany(x => x.IsActive == true && x.WebSiteid == CurrentWebsite.id));

                Language currentLanguage = CurrentLanguage;
                if (decision)
                {
                    currentLanguage = null;
                }

                if (decision && id != null)
                {
                    currentLanguage = LanguageContext.WebsiteLanguages.FirstOrDefault(x => x.id == id);
                    SetLanguage(currentLanguage);
                }
                if (currentLanguage == null)
                {
                    currentLanguage = LanguageContext.WebsiteLanguages.FirstOrDefault(x => x.Default == true);
                    if (currentLanguage == null)
                    {
                        defaultLanguage.WebSite = CurrentWebsite;
                        currentLanguage = defaultLanguage;
                        languageService.Add(currentLanguage);
                        languageService.Save();
                    }
                    SetLanguage(currentLanguage);
                }
            }
        }

        public void ChangeWebsiteThenChangeLanguage(Website website)
        {
            SetWebsite(website);
            cacheService.Remove($"{CacheInfo.Languages}");
            LoadLanguage(true);
        }


        public RedirectToActionResult PreparingPage()
        {
            return RedirectToAction("PreparingPage", "Error");
        }

        public RedirectToActionResult NotFoundPage()
        {
            return RedirectToAction("404", "Error");
        }


    }
}