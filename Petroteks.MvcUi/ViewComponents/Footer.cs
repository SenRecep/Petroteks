using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.ComplexTypes;
using Petroteks.MvcUi.ExtensionMethods;
using System;

namespace Petroteks.MvcUi.ViewComponents
{
    public class Footer : LanguageVC
    {
        private readonly IUI_FooterService uI_FooterService;

        public Footer(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            uI_FooterService = serviceProvider.GetService<IUI_FooterService>();
        }
        public IViewComponentResult Invoke()
        {
            UI_Footer uI_Footer = uI_FooterService.Get(x => x.IsActive == true && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            return View(uI_Footer);
        }
    }
}
