using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.ComplexTypes;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ViewComponents
{
    public class Footer : ViewComponent
    {
        private readonly IUI_FooterService uI_FooterService;

        public Footer(IUI_FooterService uI_FooterService)
        {
            this.uI_FooterService = uI_FooterService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UI_Footer uI_Footer = uI_FooterService.Get(x => x.IsActive == true && x.WebSiteid == WebsiteContext.CurrentWebsite.id);
            return View(uI_Footer);
        }
    }
}
