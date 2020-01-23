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
        #region Fields
        private readonly IMainPageService mainPageService;
        private readonly IAboutUsObjectService aboutUsObjectService;
        private readonly IPrivacyPolicyObjectService privacyPolicyObjectService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IBlogService blogService;
        private readonly IHostingEnvironment hostingEnvironment;
        #endregion
        #region CTOR
        public PagesController(IUserService userService,
            IUserSessionService userSessionService,
            IMainPageService mainPageService,
            IAboutUsObjectService aboutUsObjectService,
            IPrivacyPolicyObjectService privacyPolicyObjectService,
            IWebsiteService websiteService,
            IHttpContextAccessor httpContextAccessor,
            ICategoryService categoryService,
            IBlogService blogService,
            IHostingEnvironment hostingEnvironment,
            IProductService productService)
            : base(userSessionService, websiteService, httpContextAccessor)
        {
            this.mainPageService = mainPageService;
            this.aboutUsObjectService = aboutUsObjectService;
            this.privacyPolicyObjectService = privacyPolicyObjectService;
            this.categoryService = categoryService;
            this.productService = productService;
            this.blogService = blogService;
            this.hostingEnvironment = hostingEnvironment;
        }

        #endregion
        #region Pages
        [AdminAuthorize]
        [Route("Anasayfa-Duzenleme")]
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
        [Route("Anasayfa-Duzenleme")]
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
        [Route("Hakkimizda-Duzenleme")]
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
        [Route("Hakkimizda-Duzenleme")]
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
        [Route("Gizlilik-Politikasi-Duzenleme")]
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
        [Route("Gizlilik-Politikasi-Duzenleme")]
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
        [Route("Sayfa-Standarti")]
        public IActionResult SayfaStandarti()
        {
            ViewBag.ThisWebsite = ThisWebsite;
            return View();
        }

        #endregion
        #region Product
        [AdminAuthorize]
        [HttpGet]
        [Route("Urun-Olustur")]
        public IActionResult ProductAdd()
        {
            ViewBag.ThisWebsite = ThisWebsite;

            return View(new ProductViewModel());
        }


        [AdminAuthorize]
        [HttpPost]
        [Route("Urun-Olustur")]
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
                    Category rootCategory = categoryService.Get(x => x.Name == "ROOT" && x.WebSite.id == ThisWebsite.id);
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
                    IsActive = model.IsActive
                };
                productService.Add(product);
                productService.Save();
            }
            return RedirectToAction("ProductAdd", "Pages", new { area = "Admin" });
        }

        [AdminAuthorize]
        [Route("Duzenleme/Urun-{id}")]
        public IActionResult ProductEditMode(int id)
        {
            ViewBag.ThisWebsite = ThisWebsite;
            Product product = productService.Get(x => x.id == id);
            if (product != null)
                return View("ProductAdd", new ProductViewModel()
                {
                    Categoryid = product.Categoryid,
                    SupTitle = product.SupTitle,
                    SubTitle = product.SubTitle,
                    Content = product.Content,
                    Description = product.Description,
                    IsActive = product.IsActive,
                    Keywords = product.Keywords,
                    MetaTags = product.MetaTags,
                    Title = product.Title,
                    PhotoPath = product.PhotoPath
                }); ;
            return View("ProductAdd", new CategoryViewModel());

        }

        [AdminAuthorize]
        [HttpPost]
        [Route("Urun-Duzenleme")]
        public IActionResult ProductEditMode(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product findedProduct = null;
                Category category = categoryService.Get(x => x.id == model.Categoryid && x.WebSiteid == ThisWebsite.id);
                findedProduct = productService.Get(x => x.id == model.id);
                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "ProductImages");
                    uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                if (findedProduct != null)
                {
                    findedProduct.Content = model.Content;
                    findedProduct.Description = model.Description;
                    findedProduct.IsActive = model.IsActive;
                    findedProduct.Keywords = model.Keywords;
                    findedProduct.MetaTags = model.MetaTags;
                    findedProduct.SubTitle = model.SubTitle;
                    findedProduct.SupTitle = model.SupTitle;
                    findedProduct.Title = model.Title;
                    findedProduct.UpdateDate = DateTime.UtcNow;
                    findedProduct.UpdateUserid = LoginUser.id;
                    if (category != null)
                        findedProduct.Categoryid = category.id;
                    if (!string.IsNullOrWhiteSpace(uniqueFileName))
                        findedProduct.PhotoPath = uniqueFileName;
                    productService.Update(findedProduct);
                    productService.Save();
                }

            }
            return RedirectToAction("ProductAdd", "Pages", new { area = "Admin" });
        }

      
        [AdminAuthorize]
        [HttpGet]
        [Route("Urun-Silme-{id:int}")]
        public IActionResult ProductDelete(int id)
        {
            Product product = productService.Get(x => x.id == id);
            Category category = null;
            if (product != null)
                category = categoryService.Get(x => x.id == product.Categoryid && x.WebSiteid == ThisWebsite.id);
            if (category != null && product != null)
            {
                productService.Delete(product);
                productService.Save();
            }
            return RedirectToAction("ProductAdd", "Pages", new { area = "Admin" });
        }
        #endregion
        #region Blog
        [AdminAuthorize]
        [Route("Blog-Olustur")]
        public IActionResult BlogAdd()
        {
            ViewBag.ThisWebsite = ThisWebsite;
            return View(new BlogViewModel());
        }
        [AdminAuthorize]
        [HttpPost]
        [Route("Blog-Olustur")]
        public IActionResult BlogAdd(BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.PhotoPath != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "BlogImages");
                    uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + model.PhotoPath.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.PhotoPath.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Blog blog = new Blog()
                {
                    Title = model.Title,
                    PhotoPath = uniqueFileName,
                    Description = model.Description,
                    MetaTags = model.MetaTags,
                    Keywords = model.Keywords,
                    Content = model.Content,
                    CreateUserid = LoginUser.id,
                    IsActive = model.IsActive,
                    WebSite = ThisWebsite
                };
                Blog findedBlog = blogService.Get(x => x.Title.Equals(blog.Title) && x.WebSite == ThisWebsite);
                if (findedBlog != null)
                {
                    findedBlog.Description = blog.Description;
                    findedBlog.MetaTags = blog.MetaTags;
                    findedBlog.Keywords = blog.Keywords;
                    findedBlog.Content = blog.Content;
                    findedBlog.Title = blog.Title;
                    findedBlog.UpdateDate = DateTime.UtcNow;
                    findedBlog.UpdateUserid = blog.CreateUserid;
                    findedBlog.IsActive = blog.IsActive;
                    if (!string.IsNullOrWhiteSpace(blog.PhotoPath))
                        findedBlog.PhotoPath = blog.PhotoPath;
                    blogService.Update(findedBlog);
                }
                else
                {
                    blogService.Add(blog);
                    blogService.Save();
                }
            }
            return RedirectToAction("BlogAdd", "Pages", new { area = "Admin" });
        }
        [AdminAuthorize]
        [Route("Blog-Duzenleme-{id:int}")]
        public IActionResult BlogEdit(int id)
        {
            var findedBlog = blogService.Get(m => m.id == id);
            if (findedBlog != null)
            {
                return View(findedBlog);
            }
            else
            {
                return View();
            }

        }
        [AdminAuthorize]
        [Route("Blog-Silme-{id:int}")]
        public JsonResult BlogDelete(int id)
        {
            Blog blog = blogService.Get(x => x.id == id);
            if (blog != null)
            {
                blog.IsActive = false;
                blogService.Save();
                return Json("Başarılı");
            }
            return Json("Basarisiz");
        }
        [AdminAuthorize]
        [HttpPost]
        [Route("Blog-Duzenleme-{id:int}")]
        public IActionResult BlogEdit(int id, BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var findedBlog = blogService.Get(m => m.id == id);

                if (findedBlog != null)
                {
                    findedBlog.Description = model.Description;
                    findedBlog.MetaTags = model.MetaTags;
                    findedBlog.Keywords = model.Keywords;
                    findedBlog.Content = model.Content;
                    findedBlog.Title = model.Title;
                    findedBlog.UpdateDate = DateTime.UtcNow;
                    findedBlog.UpdateUserid = LoginUser.id;
                    findedBlog.IsActive = model.IsActive;
                    blogService.Update(findedBlog);
                    blogService.Save();
                }

                else
                {
                    Blog blog = new Blog()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        MetaTags = model.MetaTags,
                        Keywords = model.Keywords,
                        Content = model.Content,
                        CreateUserid = LoginUser.id,
                        IsActive = model.IsActive
                    };
                    blogService.Add(blog);
                    blogService.Save();
                }
            }

            return RedirectToAction("BlogList", "Pages", new { area = "Admin" });
        }
        [AdminAuthorize]
        [Route("Admin-Panel/Bloglar")]
        public IActionResult BlogList()
        {
            var data = blogService.GetMany(x => x.WebSiteid == ThisWebsite.id && x.IsActive == true).ToList();
            return View(data);
        }

        #endregion
        #region Category
        [AdminAuthorize]
        [HttpGet]
        [Route("Kategori-Olustur")]
        public IActionResult CategoryAdd()
        {
            ViewBag.ThisWebsite = ThisWebsite;
            return View(new CategoryViewModel());
        }

        [AdminAuthorize]
        [HttpGet]
        [Route("Duzenleme/Kategori-{id:int}")]
        public IActionResult CategoryEditMode(int id)
        {
            ViewBag.ThisWebsite = ThisWebsite;
            Category category = categoryService.Get(x => x.id == id && x.WebSiteid == ThisWebsite.id);
            if (category != null)
                return View("CategoryAdd", new CategoryViewModel() { ParentId = category.Parentid, Name = category.Name, ImagePath = category.PhotoPath });
            return View("CategoryAdd", new CategoryViewModel());

        }
        [AdminAuthorize]
        [HttpPost]
        [Route("Kategori-Duzenleme")]
        public IActionResult CategoryEditMode(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category findedCategory = categoryService.Get(x => x.id == model.id && x.WebSiteid == ThisWebsite.id);

                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "CategoryImages");
                    uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                if (findedCategory != null)
                {
                    findedCategory.Parentid = model.ParentId;
                    findedCategory.Name = model.Name;
                    findedCategory.UpdateDate = DateTime.UtcNow;
                    findedCategory.UpdateUserid = LoginUser.id;
                    if (!string.IsNullOrWhiteSpace(uniqueFileName))
                        findedCategory.PhotoPath = uniqueFileName;
                    categoryService.Update(findedCategory);
                    categoryService.Save();
                }

            }
            return RedirectToAction("CategoryAdd", "Pages", new { area = "Admin" });
        }

        [AdminAuthorize]
        [HttpPost]
        [Route("Kategori-Olustur")]
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
                categoryService.Add(category);
                categoryService.Save();
            }
            return RedirectToAction("CategoryAdd", "Pages", new { area = "Admin" });
        }

        [AdminAuthorize]
        [HttpGet]
        [Route("Kategori-Silme-{id:int}")]
        public IActionResult CategoryDelete(int id)
        {
            Category category = categoryService.Get(x => x.id == id && ThisWebsite.id == x.WebSiteid);
            if (category != null)
            {
                categoryService.Delete(category);
                categoryService.Save();
            }
            return RedirectToAction("CategoryAdd", "Pages", new { area = "Admin" });
        }

        #endregion

    }
}