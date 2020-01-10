using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Attributes;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Services;
using Petroteks.MvcUi.ViewComponents;

namespace Petroteks.MvcUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : AdminBaseController
    {
        private readonly IMainPageService mainPageService;
        private readonly IAboutUsObjectService aboutUsObjectService;
        private readonly IPrivacyPolicyObjectService privacyPolicyObjectService;
        private readonly ICategoryService categoryService;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IProductService productService;

        public PagesController(IUserService userService,
            IUserSessionService userSessionService,
            IMainPageService mainPageService,
            IAboutUsObjectService aboutUsObjectService,
            IPrivacyPolicyObjectService privacyPolicyObjectService,
            IWebsiteService websiteService,
            IHttpContextAccessor httpContextAccessor,
            ICategoryService categoryService,
            IHostingEnvironment hostingEnvironment,
            IProductService productService)
            : base(userSessionService, websiteService, httpContextAccessor)
        {
            this.mainPageService = mainPageService;
            this.aboutUsObjectService = aboutUsObjectService;
            this.privacyPolicyObjectService = privacyPolicyObjectService;
            this.categoryService = categoryService;
            this.hostingEnvironment = hostingEnvironment;
            this.productService = productService;
        }



        [AdminAuthorize]
        public IActionResult AnaSayfaEdit()
        {
            MainPage mainPage;
            mainPage = mainPageService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (mainPage == null)
                mainPage = new MainPage();
            return View(mainPage);
        }


        [AdminAuthorize]
        [HttpPost]
        public IActionResult AnaSayfaEdit(MainPage model)
        {
            MainPage mainPage;
            mainPage = mainPageService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (mainPage == null)
            {
                mainPage = model;
                mainPage.CreateUserid = LoginUser.id;
                mainPage.WebSite = ThisWebsite;
                mainPageService.Add(mainPage);
            }
            else
            {
                mainPage.Keywords = model.Keywords;
                mainPage.Title = model.Title;
                mainPage.MetaTags = model.MetaTags;
                mainPage.TopContent = model.TopContent;
                mainPage.BottomContent = model.BottomContent;
                mainPage.Description = model.Description;
                mainPage.Slider = model.Slider;
                mainPage.UpdateUserid = LoginUser.id;
                mainPage.UpdateDate = DateTime.UtcNow;
                mainPageService.Update(mainPage);
            }
            mainPageService.Save();
            return View(mainPage);
        }




        [AdminAuthorize]
        public IActionResult HakkimizdaEdit()
        {
            AboutUsObject aboutus;
            aboutus = aboutUsObjectService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (aboutus == null)
                aboutus = new AboutUsObject();
            return View(aboutus);
        }


        [AdminAuthorize]
        [HttpPost]
        public IActionResult HakkimizdaEdit(AboutUsObject model)
        {
            AboutUsObject aboutus;
            aboutus = aboutUsObjectService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (aboutus == null)
            {
                aboutus = model;
                aboutus.WebSite = ThisWebsite;
                aboutus.CreateUserid = LoginUser.id;
                aboutUsObjectService.Add(aboutus);
            }
            else
            {
                aboutus.Keywords = model.Keywords;
                aboutus.Title = model.Title;
                aboutus.MetaTags = model.MetaTags;
                aboutus.Description = model.Description;
                aboutus.Content = model.Content;
                aboutus.UpdateUserid = LoginUser.id;
                aboutus.UpdateDate = DateTime.UtcNow;
                aboutUsObjectService.Update(aboutus);
            }
            aboutUsObjectService.Save();
            return View(aboutus);
        }




        [AdminAuthorize]
        public IActionResult GizlilikPolitikasiEdit()
        {
            PrivacyPolicyObject privacyPage;
            privacyPage = privacyPolicyObjectService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (privacyPage == null)
                privacyPage = new PrivacyPolicyObject();
            return View(privacyPage);
        }


        [AdminAuthorize]
        [HttpPost]
        public IActionResult GizlilikPolitikasiEdit(PrivacyPolicyObject model)
        {
            PrivacyPolicyObject privacyPage;
            privacyPage = privacyPolicyObjectService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (privacyPage == null)
            {
                privacyPage = model;
                privacyPage.WebSite = ThisWebsite;
                privacyPage.CreateUserid = LoginUser.id;
                privacyPolicyObjectService.Add(privacyPage);
            }
            else
            {
                privacyPage.Keywords = model.Keywords;
                privacyPage.Title = model.Title;
                privacyPage.MetaTags = model.MetaTags;
                privacyPage.Description = model.Description;
                privacyPage.Content = model.Content;
                privacyPage.UpdateUserid = LoginUser.id;
                privacyPage.UpdateDate = DateTime.UtcNow;
                privacyPolicyObjectService.Update(privacyPage);
            }
            privacyPolicyObjectService.Save();
            return View(privacyPage);
        }


        [AdminAuthorize]
        [HttpGet]
        public IActionResult SayfaStandarti()
        {
            ViewBag.ThisWebsite = ThisWebsite;

            return View(new ProductViewModel());
        }

        [AdminAuthorize]
        [HttpGet]
        public IActionResult ProductAdd()
        {
            ViewBag.ThisWebsite = ThisWebsite;

            return View(new ProductViewModel());
        }

        [AdminAuthorize]
        [HttpPost]
        public IActionResult ProductAdd(ProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "ProductImages");
                    uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Category category;
                category = categoryService.Get(x => x.id == model.Categoryid);
                if (category == null && model.Categoryid == 0)
                {
                    Category rootCategory = categoryService.Get(x => x.Name == "ROOT" && x.WebSite == ThisWebsite);
                    if (rootCategory == null)
                    {
                        rootCategory = new Category()
                        {
                            Name = "ROOT",
                            Parentid = 0,
                            WebSite = ThisWebsite,
                        };
                        categoryService.Add(rootCategory);
                        categoryService.Save();
                    }
                    category = rootCategory;
                }
                Product product = new Product()
                {
                    SupTitle = model.SupTitle,
                    SubTitle = model.SubTitle,
                    Category = category,
                    PhotoPath = uniqueFileName,
                    Description = model.Description,
                    MetaTags = model.MetaTags,
                    Keywords = model.Keywords,
                    Content = model.Content,
                    Title = model.Title,
                    CreateUserid = LoginUser.id,
                    IsActive=model.IsActive
                };
                Product findedProduct = productService.Get(x => x.SupTitle.Equals(product.SupTitle) && x.Category.WebSite == ThisWebsite);
                if (findedProduct != null)
                {
                    findedProduct.SupTitle = product.SupTitle;
                    findedProduct.SubTitle = product.SubTitle;
                    findedProduct.Category = product.Category;
                    findedProduct.Description = product.Description;
                    findedProduct.MetaTags = product.MetaTags;
                    findedProduct.Keywords = product.Keywords;
                    findedProduct.Content = product.Content;
                    findedProduct.Title = product.Title;
                    findedProduct.UpdateDate = DateTime.UtcNow;
                    findedProduct.UpdateUserid = product.CreateUserid;
                    findedProduct.IsActive = product.IsActive;
                    if (!string.IsNullOrWhiteSpace(product.PhotoPath))
                        findedProduct.PhotoPath = product.PhotoPath;
                    productService.Update(findedProduct);
                }
                else
                    productService.Add(product);
                productService.Save();
            }
            return RedirectToAction("ProductAdd", "Pages", new { area = "Admin" });
        }
        public IActionResult ProductList()
        {
            return View();
        }
        [AdminAuthorize]
        [HttpGet]
        public IActionResult CategoryAdd()
        {
            ViewBag.ThisWebsite = ThisWebsite;
            return View();
        }
        [AdminAuthorize]
        [HttpPost]
        public IActionResult CategoryAdd(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "CategoryImages");
                    uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Category category = new Category()
                {
                    Name = model.Name,
                    Parentid = model.ParentId,
                    PhotoPath = uniqueFileName,
                    WebSite = ThisWebsite,
                    CreateUserid = LoginUser.id
                };
                Category findedCategory = categoryService.Get(x => x.Name.Equals(category.Name) && x.WebSite == ThisWebsite);
                if (findedCategory != null)
                {
                    findedCategory.Parentid = category.Parentid;
                    findedCategory.UpdateDate = DateTime.UtcNow;
                    findedCategory.UpdateUserid = category.CreateUserid;
                    if (!string.IsNullOrWhiteSpace(category.PhotoPath))
                        findedCategory.PhotoPath = category.PhotoPath;
                    categoryService.Update(findedCategory);
                }
                else
                    categoryService.Add(category);
                categoryService.Save();
            }
            return RedirectToAction("CategoryAdd", "Pages", new { area = "Admin" });
        }
    }
}