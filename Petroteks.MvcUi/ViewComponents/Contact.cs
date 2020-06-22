using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.ComplexTypes;
using Petroteks.MvcUi.ExtensionMethods;
using System;

namespace Petroteks.MvcUi.ViewComponents
{
    public class Contact : LanguageVC
    {
        private readonly IUI_ContactService uI_ContactService;
        public Contact(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            uI_ContactService = serviceProvider.GetService<IUI_ContactService>();
        }
        public IViewComponentResult Invoke()
        {
            UI_Contact uI_Contact = uI_ContactService.Get(x => x.IsActive == true && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            return View(uI_Contact);
        }
    }
}
