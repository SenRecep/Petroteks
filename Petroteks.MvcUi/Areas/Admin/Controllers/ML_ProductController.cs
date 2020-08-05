using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.MvcUi.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Petroteks.Bll.Helpers;
using Petroteks.MvcUi.Areas.Admin.Models;
using Petroteks.MvcUi.Attributes;
using Petroteks.Entities.Concreate;
using Newtonsoft.Json;
using Petroteks.Entities.ComplexTypes;

namespace Petroteks.MvcUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ML_ProductController : AdminBaseController
    {
        private readonly IProductService productService;
        private readonly IML_ProductService mL_ProductService;
        private readonly ILanguageService languageService;
        private readonly ICategoryService categoryService;
        public ML_ProductController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.productService = serviceProvider.GetService<IProductService>();
            this.mL_ProductService = serviceProvider.GetService<IML_ProductService>();
            this.languageService = serviceProvider.GetService<ILanguageService>();
            this.categoryService = serviceProvider.GetService<ICategoryService>();
        }
        [Route("AlternatifUrunBelirleme")]
        [AdminAuthorize]
        public IActionResult MLProductSync()
        {
            var mL_Products = mL_ProductService.GetAllActiveLoaded();
            var all = getModel();
            var model = (ML_Products: mL_Products, All: all);
            return View(model);
        }

        private List<WLCViewModel> getModel()
        {
            List<WLCViewModel> wLCViewModels = new List<WLCViewModel>();
            var languages = LanguageContext.WebsiteLanguages;
            var categories = categoryService.GetMany(x => x.IsActive && x.WebSiteid == CurrentWebsiteId);
            var _products = productService.GetMany(x => x.IsActive);
            var products = from category in categories
                           join prod in _products on category.id equals prod.Categoryid
                           select prod;

            foreach (var lang in languages)
            {
                List<WCPViewModel> wCPViewModels = new List<WCPViewModel>();

                var langCategories = categories.Where(x => x.Languageid == lang.id).ToList();

                if (langCategories.Count == 0)
                    continue;

                foreach (var category in langCategories)
                {
                    var langProducts = products.Where(x => x.Categoryid == category.id).ToList();

                    if (langProducts.Count == 0)
                        continue;
                    WCPViewModel wCPViewModel = new WCPViewModel()
                    {
                        Category = category,
                        Products = langProducts
                    };
                    wCPViewModels.Add(wCPViewModel);
                }

                if (wCPViewModels.Count == 0)
                    continue;

                WLCViewModel wLCViewModel = new WLCViewModel()
                {
                    Language = lang,
                    Categories = wCPViewModels
                };
                wLCViewModels.Add(wLCViewModel);


            }
            return wLCViewModels;
        }

        [HttpPost]
        public IActionResult GetProductsAjax(AlternateProductViewModel left, AlternateProductViewModel right)
        {
            Product productLeft = productService.Get(x => x.IsActive && x.id == left.ProductId && x.Languageid == left.LanguageId);
            var pleft = new { productLeft.id, productLeft.PhotoPath, productLeft.SupTitle, left.LanguageId, left.KeyCode };
            Product productRight = productService.Get(x => x.IsActive && x.id == right.ProductId && x.Languageid == right.LanguageId);
            var pright = new { productRight.id, productRight.PhotoPath, productRight.SupTitle, right.LanguageId, right.KeyCode };
            var json = JsonConvert.SerializeObject(new { pLeft = pleft, pRight = pright });
            return Json(json);
        }


        [HttpPost]
        public IActionResult SaveProducts(AlternateProductViewModel left, AlternateProductViewModel right)
        {
            Product productLeft = productService.Get(x => x.IsActive && x.id == left.ProductId && x.Languageid == left.LanguageId);
            Product productRight = productService.Get(x => x.IsActive && x.id == right.ProductId && x.Languageid == right.LanguageId);

            if (productLeft!=null && productRight!=null)
            {
                ML_Product mL_Product = new ML_Product()
                {
                    CreateUserid = LoginUser.id,
                    WebSiteid = CurrentWebsiteId,
                    ProductId = productLeft.id,
                    Product = productLeft,
                    AlternateProduct = productRight,
                    AlternateProductId = productRight.id,
                    ProductLanguageKeyCode = left.KeyCode,
                    AlternateProductLanguageKeyCode = right.KeyCode
                };

                mL_ProductService.Add(mL_Product);
                mL_ProductService.Save();
                return Json("Islem Basarili");

            }
            return Json("Islem Basarısız");


        }
    }
}
