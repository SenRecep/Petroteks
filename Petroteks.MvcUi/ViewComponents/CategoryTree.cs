using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ViewComponents
{
    public class CategoryTree:ViewComponent
    {
        private readonly ICategoryService categoryService;

        public CategoryTree(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Website website)
        {
            return View(new CategoryListViewModel(categoryService)
            {
                MainCategories = categoryService.GetMany(category => category.WebSiteid == website.id && category.Parentid == 0),
                AllSubCategory = categoryService.GetMany(category => category.WebSiteid == website.id && category.Parentid != 0)
            });
        }
    }
}
