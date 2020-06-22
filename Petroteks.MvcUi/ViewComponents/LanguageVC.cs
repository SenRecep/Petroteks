using Microsoft.AspNetCore.Mvc;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Services;
using System;

namespace Petroteks.MvcUi.ViewComponents
{
    public abstract class LanguageVC : ViewComponent
    {
        private readonly ILanguageCookieService languageCookieService;
        private readonly IWebsiteCookieService websiteCookieService;

        public LanguageVC(IServiceProvider serviceProvider)
        {
            languageCookieService = serviceProvider.GetService<ILanguageCookieService>();
            websiteCookieService = serviceProvider.GetService<IWebsiteCookieService>();
        }
        public LanguageVC(ILanguageCookieService languageCookieService)
        {
            this.languageCookieService = languageCookieService;
        }

        public Language CurrentLanguage => languageCookieService.Get("CurrentLanguage");
        public Website CurrentWebsite => websiteCookieService.Get("CurrentWebsite");
    }
}
