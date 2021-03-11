using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Petroteks.Bll.Abstract;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Models.MI;

namespace Petroteks.MvcUi.ViewComponents
{
    public class CategoryTree : LanguageVC
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public CategoryTree(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            categoryService = serviceProvider.GetService<ICategoryService>();
            productService = serviceProvider.GetService<IProductService>();
        }

        public IViewComponentResult Invoke()
        {
            ICollection<MI_Category> categories = categoryService
                           .GetMany(category => category.WebSiteid == CurrentWebsite.id && category.IsActive == true, CurrentLanguage.id)
                           .Select(x => new MI_Category(x))
                           .OrderByDescending(x => x.Priority)
                           .ToList();

            ICollection<MI_Product> products = productService
                .GetMany(x => x.IsActive == true, CurrentLanguage.id)
                .Select(x => new MI_Product(x))
                .OrderByDescending(x => x.Priority)
                .ToList();

            return View(new CategoryListViewModel(categoryService, productService)
            {
                MainCategories = categories.Where(x => x.Parentid == 0).ToList(),
                AllSubCategory = categories.Where(x => x.Parentid != 0).ToList(),
                AllProduct = products
            });

        }
    }
}
