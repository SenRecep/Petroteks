using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Petroteks.MvcUi.Controllers
{
    public class DetailController : GlobalController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public DetailController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            productService = serviceProvider.GetService<IProductService>();
            categoryService = serviceProvider.GetService<ICategoryService>();
        }

        [Route("Kategori-Detay/{categoryName}-{page:int}-{category:int}")]
        public IActionResult CategoryDetail(int page = 1, int category = 0)
        {
            Category Category = categoryService.GetAllLanguageCategory(x => x.id == category && x.WebSite == CurrentWebsite && x.IsActive == true);
            if (Category != null)
            {
                int pagesize = 10;
                List<Category> subCategories = categoryService.GetMany(x => x.WebSite == CurrentWebsite && x.Parentid == Category.id && x.IsActive == true, Category.Languageid.Value).OrderByDescending(x => x.Priority).ToList();
                ICollection<Product> products = productService.GetMany(x => x.Categoryid == Category.id && x.IsActive == true, Category.Languageid.Value);
                if (Category.Languageid!=CurrentLanguage.id)
                    LoadLanguage(true,Category.Languageid);
                return View(new ProductListViewModel()
                {
                    Products = products.Skip((page - 1) * pagesize).Take(pagesize).OrderByDescending(x => x.Priority).ToList(),
                    PageCount = (int)Math.Ceiling(products.Count / (double)pagesize),
                    PageSize = pagesize,
                    CurrentCategory = Category,
                    CurrentPage = page,
                    SubCategories = subCategories
                });
            }
            else
            {
                return RedirectToAction("CategoryNotFound");
            }
        }
        [Route("Urun-Detay/{produtname}-{id:int}")]
        [HttpGet]
        public IActionResult ProductDetail(int id)
        {
            Product product = productService.GetAllLanguageProduct(x => x.id == id && x.IsActive == true);
            if (product != null)
            {
                Category category = categoryService.GetAllLanguageCategory(x => x.IsActive && x.WebSiteid == CurrentWebsite.id && x.id == product.Categoryid);
                if (category != null)
                {
                    if (product?.Languageid != CurrentLanguage.id)
                    {
                        LoadLanguage(true, product.Languageid);
                    }

                    return View(product);
                }
            }
            return RedirectToAction("ProductNotFound");
        }
        [Route("404-Product-Not-Found.html")]
        [HttpGet]
        public IActionResult ProductNotFound()
        {
            return View();
        }
        [Route("404-Category-Not-Found.html")]
        [HttpGet]
        public IActionResult CategoryNotFound()
        {
            return View();
        }
    }
}


