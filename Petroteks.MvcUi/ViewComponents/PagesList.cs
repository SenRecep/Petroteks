using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using System.Collections.Generic;

namespace Petroteks.MvcUi.ViewComponents
{
    public class PagesList : ViewComponent
    {
        private readonly IDynamicPageService dynamicPageService;

        public PagesList(IDynamicPageService dynamicPageService)
        {
            this.dynamicPageService = dynamicPageService;
        }
        public IViewComponentResult Invoke(Website website)
        {
            ICollection<DynamicPage> dynamicPages = dynamicPageService.GetMany(x => x.WebSiteid == website.id && x.IsActive == true);

            return View(dynamicPages);
        }
    }
}
