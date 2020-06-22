using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.ComplexTypes;
using Petroteks.MvcUi.ExtensionMethods;
using System;

namespace Petroteks.MvcUi.ViewComponents
{
    public class Navbar : LanguageVC
    {
        private readonly IUI_NavbarService uI_NavbarService;

        public Navbar(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            uI_NavbarService = serviceProvider.GetService<IUI_NavbarService>();
        }
        public IViewComponentResult Invoke()
        {
            UI_Navbar uI_Navbar = uI_NavbarService.Get(x => x.IsActive == true && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            return View(uI_Navbar);
        }
    }
}
