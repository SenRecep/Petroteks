using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ViewComponents
{
    public class CategoryTree:ViewComponent
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public CategoryTree(ICategoryService categoryService,IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Website website)
        {
            ICollection<Category> categories = categoryService.GetMany(category => category.WebSiteid == website.id && category.IsActive == true);
            ICollection<Product> products = Products(categories).OrderByDescending(x=>x.Priority).ToList();
            return View(new CategoryListViewModel(categoryService, productService)
            {
                MainCategories = categories.Where(x => x.Parentid == 0).OrderByDescending(x => x.Priority).ToList(),
                AllSubCategory = categories.Where(x => x.Parentid != 0).OrderByDescending(x => x.Priority).ToList(),
                AllProduct = products
            });

        }

        public IEnumerable<Product> Products(ICollection<Category> categories)
        {
            var products = productService.GetMany(x => x.IsActive == true);
            foreach (Product item in products)
            {
                Category category = categories.Where(x => x.id == item.Categoryid).FirstOrDefault();
                if (category != null && item.IsActive == true)
                    yield return item;
            }
        }
    }
}
