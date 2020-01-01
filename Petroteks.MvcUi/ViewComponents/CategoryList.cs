using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ViewComponents
{
    public class CategoryList:ViewComponent
    {
        private readonly ICategoryService categoryService;
        private readonly Website website;

        public CategoryList(ICategoryService categoryService, Website website)
        {
            this.categoryService = categoryService;
            this.website = website;
        }
        public Category GetCategory(int parentId)
        {
            return categoryService.Get(x => x.id == parentId && x.WebSiteid == website.id);
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
