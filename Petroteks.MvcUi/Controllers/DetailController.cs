using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Concreate;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Petroteks.MvcUi.Controllers
{
    public class DetailController : GlobalController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IRouteTable routeTable;

        public DetailController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            productService = serviceProvider.GetService<IProductService>();
            categoryService = serviceProvider.GetService<ICategoryService>();
            routeTable = serviceProvider.GetService<IRouteTable>();
        }

        #region Sayfalamali kategori
        //[Route("{pageTag}/{categoryName}-{page:int}_{category:int}")]
        //public IActionResult CategoryDetail(string pageTag, int page = 1, int category = 0)
        //{
        //    if (routeTable.Exists(pageTag, EntityName.Category, PageType.Detail))
        //    {
        //        Category Category = categoryService.GetAllLanguageCategory(x => x.id == category && x.WebSite == CurrentWebsite && x.IsActive == true);
        //        if (Category != null)
        //        {
        //            int pagesize = 10;
        //            List<Category> subCategories = categoryService.GetMany(x => x.WebSite == CurrentWebsite && x.Parentid == Category.id && x.IsActive == true, Category.Languageid.Value).OrderByDescending(x => x.Priority).ToList();
        //            ICollection<Product> products = productService.GetMany(x => x.Categoryid == Category.id && x.IsActive == true, Category.Languageid.Value);
        //            if (Category.Languageid != CurrentLanguage.id)
        //                LoadLanguage(true, Category.Languageid);
        //            return View(new ProductListViewModel()
        //            {
        //                Products = products.Skip((page - 1) * pagesize).Take(pagesize).OrderByDescending(x => x.Priority).ToList(),
        //                PageCount = (int)Math.Ceiling(products.Count / (double)pagesize),
        //                PageSize = pagesize,
        //                CurrentCategory = Category,
        //                CurrentPage = page,
        //                SubCategories = subCategories
        //            });
        //        }
        //        else
        //        {
        //            return RedirectToAction("CategoryNotFound");
        //        }
        //    }
        //    else return NotFoundPage();
        //} 
        #endregion


        [Route("{pageTag}/{categoryName}/{id:int}")]
        public IActionResult CategoryDetail(string pageTag, int id )
        {
            if (routeTable.Exists(pageTag, EntityName.Category, PageType.Detail))
            {
                Category Category = categoryService.GetAllLanguageCategory(x => x.id == id && x.WebSite == CurrentWebsite && x.IsActive == true);
                if (Category != null)
                {
                    List<Category> subCategories = categoryService.GetMany(x => x.WebSite == CurrentWebsite && x.Parentid == Category.id && x.IsActive == true, Category.Languageid.Value).OrderByDescending(x => x.Priority).ToList();
                    ICollection<Product> products = productService.GetMany(x => x.Categoryid == Category.id && x.IsActive == true, Category.Languageid.Value);
                    if (Category.Languageid != CurrentLanguage.id)
                        LoadLanguage(true, Category.Languageid);
                    return View(new ProductListViewModel()
                    {
                        Products = products,
                        CurrentCategory = Category,
                        SubCategories = subCategories
                    });
                }
                else
                    return RedirectToAction("CategoryNotFound");
            }
            else 
                return NotFoundPage();
        }

        [Route("{pageTag}/{produtname}-{id:int}")]
        [HttpGet]
        public IActionResult ProductDetail(string pageTag, int id)
        {
            if (routeTable.Exists(pageTag, EntityName.Product, PageType.Detail))
            {
                Product product = productService.GetAllLanguageProduct(x => x.id == id && x.IsActive == true);
                if (product != null)
                {
                    Category category = categoryService.GetAllLanguageCategory(x => x.IsActive && x.WebSiteid == CurrentWebsite.id && x.id == product.Categoryid);
                    if (category != null)
                    {
                        if (product?.Languageid != CurrentLanguage.id)
                            LoadLanguage(true, product.Languageid);
                        return View(product);
                    }
                }
                return RedirectToAction("ProductNotFound");
            }
            else
                return NotFoundPage();
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


