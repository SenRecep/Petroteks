using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace Petroteks.MvcUi.ViewComponents
{
    public class PagesList : LanguageVC
    {
        private readonly IDynamicPageService dynamicPageService;

        public PagesList(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            dynamicPageService = serviceProvider.GetService<IDynamicPageService>();
        }
        public IViewComponentResult Invoke()
        {
            ICollection<DynamicPage> dynamicPages = dynamicPageService.GetMany(x => x.WebSiteid == CurrentWebsite.id && x.IsActive == true, CurrentLanguage.id);

            return View(dynamicPages);
        }
    }
}
