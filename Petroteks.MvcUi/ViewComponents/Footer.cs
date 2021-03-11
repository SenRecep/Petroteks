using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using Petroteks.Bll.Abstract;
using Petroteks.Entities.ComplexTypes;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Services;
using Petroteks.MvcUi.StringInfos;

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ViewComponents
{
    public class Footer : LanguageVC
    {
        private readonly IUI_FooterService uI_FooterService;
        private readonly ICacheService cacheService;

        public Footer(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            uI_FooterService = serviceProvider.GetService<IUI_FooterService>();
            cacheService = serviceProvider.GetService<ICacheService>();
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UI_Footer uI_Footer = await cacheService.GetAsync($"{CacheInfo.Footer}-{CurrentWebsite.id}-{CurrentLanguage.id}", () =>
                 uI_FooterService.Get(x => x.IsActive == true && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id)
            );
            return View(uI_Footer);
        }
    }
}
