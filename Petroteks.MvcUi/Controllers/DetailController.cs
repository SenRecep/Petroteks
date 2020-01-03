using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;

namespace Petroteks.MvcUi.Controllers
{
    public class DetailController : PublicController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public DetailController(IProductService productService,ICategoryService categoryService, IWebsiteService websiteService, IHttpContextAccessor httpContextAccessor) :base(websiteService, httpContextAccessor)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }



        public IActionResult CategoryDetail(string categoryName, int page=1,int category=0)
        {
            int pagesize = 10;
            var Category = categoryService.Get(x=>x.id==category && x.WebSite==ThisWebsite);
            var products = productService.GetMany(x=>x.Categoryid==Category.id && x.IsActive==true);
            return View(new ProductListViewModel() { 
                    Products=products.Skip((page-1)*pagesize).Take(pagesize).ToList(),
                    PageCount=(int)Math.Ceiling(products.Count/(double)pagesize),
                    PageSize=pagesize,
                    CurrentCategory=Category,
                    CurrentPage=page
            });
        }
        [HttpGet]
        public IActionResult ProductDetail(string produtname,int product)
        {
            Product Product = productService.Get(x=>x.id==product); 
            return View(Product);
        }
    }
}