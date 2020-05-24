using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Petroteks.MvcUi.Controllers
{
    public class DetailController : PublicController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public DetailController(IProductService productService, ILanguageService languageService, ICategoryService categoryService, IWebsiteService websiteService, ILanguageCookieService languageCookieService, IHttpContextAccessor httpContextAccessor) : base(websiteService, languageService, languageCookieService, httpContextAccessor)
        {
            this.productService = productService;
            this.categoryService = categoryService;


        }
        [Route("Kategori-Detay/{categoryName}-{page:int}-{category:int}")]
        public IActionResult CategoryDetail(int page = 1, int category = 0)
        {
            int pagesize = 10;
            Category Category = categoryService.Get(x => x.id == category && x.WebSite == Bll.Helpers.WebsiteContext.CurrentWebsite && x.IsActive == true);
            List<Category> subCategories = categoryService.GetMany(x => x.WebSite == Bll.Helpers.WebsiteContext.CurrentWebsite && x.Parentid == Category.id && x.IsActive == true).OrderByDescending(x => x.Priority).ToList();
            ICollection<Product> products = productService.GetMany(x => x.Categoryid == Category.id && x.IsActive == true);
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
        [Route("Urun-Detay/{produtname}-{id:int}")]
        [HttpGet]
        public IActionResult ProductDetail(int id)
        {
            Product product = productService.GetAllLanguageProduct(x => x.id == id && x.IsActive == true);
            if (product != null)
            {
                Category category = categoryService.GetAllLanguageCategory(x => x.IsActive && x.WebSiteid == WebsiteContext.CurrentWebsite.id && x.id == product.Categoryid);
                if (category != null)
                {
                    if (product?.Languageid != LanguageContext.CurrentLanguage.id)
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
    }
}


