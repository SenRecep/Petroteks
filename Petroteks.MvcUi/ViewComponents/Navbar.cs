using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.ComplexTypes;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ViewComponents
{
    public class Navbar : ViewComponent
    {
        private readonly IUI_NavbarService uI_NavbarService;

        public Navbar(IUI_NavbarService uI_NavbarService)
        {
            this.uI_NavbarService = uI_NavbarService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UI_Navbar uI_Navbar = uI_NavbarService.Get(x => x.IsActive == true && x.WebSiteid == WebsiteContext.CurrentWebsite.id);
            return View(uI_Navbar);
        }
    }
}
