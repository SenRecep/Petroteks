using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Abstract;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ViewComponents
{
    public class PagesList:ViewComponent
    {
        private readonly IDynamicPageService dynamicPageService;

        public PagesList(IDynamicPageService dynamicPageService )
        {
            this.dynamicPageService = dynamicPageService;
        }
        public IViewComponentResult Invoke(Website website)
        {
            ICollection<DynamicPage> dynamicPages = dynamicPageService.GetMany(x=>x.WebSiteid==website.id && x.IsActive==true);

            return View(dynamicPages);
        }
    }
}
