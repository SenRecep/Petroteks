using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Models.MI;
using Petroteks.MvcUi.Services;
using Petroteks.MvcUi.StringInfos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ViewComponents
{
    public class CategoryList : LanguageVC
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly ICacheService cacheService;

        public CategoryList(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            categoryService = serviceProvider.GetService<ICategoryService>();
            productService = serviceProvider.GetService<IProductService>();
            cacheService = serviceProvider.GetService<ICacheService>();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ICollection<MI_Category> categories = await cacheService.GetAsync($"{CacheInfo.OrderedCategories}-{CurrentWebsite.id}-{CurrentLanguage.id}", () =>
                categoryService
                .GetMany(category => category.WebSiteid == CurrentWebsite.id && category.IsActive == true, CurrentLanguage.id)
                .OrderByDescending(x => x.Priority)
                .Select(x => new MI_Category(x))
                .ToList()
            );


            ICollection<MI_Product> products = await cacheService.GetAsync($"{CacheInfo.OrderedProducts}-{CurrentWebsite.id}-{CurrentLanguage.id}", () =>
              productService
                .GetMany(x => x.IsActive == true, CurrentLanguage.id)
                .Select(x => new MI_Product(x))
                .OrderByDescending(x => x.Priority)
                .ToList()
           );

            return View(new CategoryListViewModel(categoryService, productService)
            {
                MainCategories = categories.Where(x => x.Parentid == 0).ToList(),
                AllSubCategory = categories.Where(x => x.Parentid != 0).ToList(),
                AllProduct = products
            });
        }
    }
}
