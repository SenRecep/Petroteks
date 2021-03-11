using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.ComplexTypes;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Services;
using Petroteks.MvcUi.StringInfos;

using System;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ViewComponents
{
    public class Navbar : LanguageVC
    {
        private readonly IUI_NavbarService uI_NavbarService;
        private readonly ICacheService cacheService;

        public Navbar(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            uI_NavbarService = serviceProvider.GetService<IUI_NavbarService>();
            cacheService = serviceProvider.GetService<ICacheService>();
        }
        public  async Task<IViewComponentResult> InvokeAsync()
        {
            UI_Navbar uI_Navbar = await cacheService.GetAsync($"{CacheInfo.Navbar}-{CurrentWebsite.id}-{CurrentLanguage.id}", () =>
                 uI_NavbarService.Get(x => x.IsActive == true && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id)
            );
            return View(uI_Navbar);
        }
    }
}
