using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.ComplexTypes;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ViewComponents
{
    public class Contact : ViewComponent
    {
        private readonly IUI_ContactService uI_ContactService;

        public Contact(IUI_ContactService uI_ContactService)
        {
            this.uI_ContactService = uI_ContactService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UI_Contact uI_Contact = uI_ContactService.Get(x => x.IsActive == true && x.WebSiteid == WebsiteContext.CurrentWebsite.id);
            return View(uI_Contact);
        }
    }
}
