using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Areas.Admin.Models;
using Petroteks.MvcUi.Attributes;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Services;
using System;
using System.IO;
using System.Linq;

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
        private readonly IUI_NavbarService uI_NavbarService;
        private readonly IUI_FooterService uI_FooterService;
        private readonly IUI_ContactService uI_ContactService;
        private readonly ILanguageService languageService;
        private readonly IHostingEnvironment hostingEnvironment;
        #endregion
        #region CTOR
        public PagesController(
            IUserService userService,
            IUserSessionService userSessionService,
            IMainPageService mainPageService,
            IAboutUsObjectService aboutUsObjectService,
            IPrivacyPolicyObjectService privacyPolicyObjectService,
            IWebsiteService websiteService,
            IHttpContextAccessor httpContextAccessor,
            ICategoryService categoryService,
            IBlogService blogService,
            ILanguageService languageService,
            IUI_NavbarService uI_NavbarService,
            IUI_FooterService uI_FooterService,
            IUI_ContactService uI_ContactService,
            IHostingEnvironment hostingEnvironment,

            ILanguageCookieService languageCookieService,

            IProductService productService)
            : base(userSessionService, websiteService, languageService, languageCookieService, httpContextAccessor)
        {
            this.mainPageService = mainPageService;
            this.aboutUsObjectService = aboutUsObjectService;
            this.privacyPolicyObjectService = privacyPolicyObjectService;
            this.categoryService = categoryService;
            this.productService = productService;
            this.blogService = blogService;
            this.uI_NavbarService = uI_NavbarService;
            this.uI_FooterService = uI_FooterService;
            this.uI_ContactService = uI_ContactService;
            this.languageService = languageService;
            this.hostingEnvironment = hostingEnvironment;
        }

        #endregion
        #region Pages
        [AdminAuthorize]
        [Route("Anasayfa-Duzenleme")]
        public IActionResult AnaSayfaEdit()
        {
            MainPage mainPage;
            mainPage = mainPageService.Get(x => x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
            if (mainPage == null)
            {
                mainPage = new MainPage();
            }

            return View(mainPage);
        }


        [AdminAuthorize]
        [HttpPost]
        [Route("Anasayfa-Duzenleme")]
        public IActionResult AnaSayfaEdit(MainPage model)
        {
            MainPage mainPage;
            mainPage = mainPageService.Get(x => x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
            if (mainPage == null)
            {
                mainPage = model;
                mainPage.CreateUserid = LoginUser.id;
                mainPage.WebSite = Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite;
                mainPage.Language = LanguageContext.CurrentLanguage;
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
                mainPage.Language = LanguageContext.CurrentLanguage;
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
            aboutus = aboutUsObjectService.Get(x => x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
            if (aboutus == null)
            {
                aboutus = new AboutUsObject();
            }

            return View(aboutus);
        }

        [AdminAuthorize]
        [HttpPost]
        [Route("Hakkimizda-Duzenleme")]
        public IActionResult HakkimizdaEdit(AboutUsObject model)
        {
            AboutUsObject aboutus;
            aboutus = aboutUsObjectService.Get(x => x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
            if (aboutus == null)
            {
                aboutus = model;
                aboutus.WebSite = Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite;
                aboutus.CreateUserid = LoginUser.id;
                aboutus.Language = LanguageContext.CurrentLanguage;
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
                aboutus.Language = LanguageContext.CurrentLanguage;
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
            privacyPage = privacyPolicyObjectService.Get(x => x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
            if (privacyPage == null)
            {
                privacyPage = new PrivacyPolicyObject();
            }

            return View(privacyPage);
        }


        [AdminAuthorize]
        [HttpPost]
        [Route("Gizlilik-Politikasi-Duzenleme")]
        public IActionResult GizlilikPolitikasiEdit(PrivacyPolicyObject model)
        {
            PrivacyPolicyObject privacyPage;
            privacyPage = privacyPolicyObjectService.Get(x => x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
            if (privacyPage == null)
            {
                privacyPage = model;
                privacyPage.WebSite = Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite;
                privacyPage.CreateUserid = LoginUser.id;
                privacyPage.Language = LanguageContext.CurrentLanguage;
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
                privacyPage.Language = LanguageContext.CurrentLanguage;
                privacyPolicyObjectService.Update(privacyPage);
            }
            privacyPolicyObjectService.Save();
            return View(privacyPage);
        }


        [AdminAuthorize]
        [Route("Sayfa-Standarti")]
        public IActionResult SayfaStandarti()
        {

            return View();
        }

        #endregion
        #region Product
        [AdminAuthorize]
        [HttpGet]
        [Route("Urun-Olustur")]
        public IActionResult ProductAdd()
        {
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
                    Category rootCategory = categoryService.Get(x => x.Name == "ROOT" && x.WebSite.id == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
                    if (rootCategory == null)
                    {
                        rootCategory = new Category()
                        {
                            Name = "ROOT",
                            Parentid = 0,
                            WebSite = Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite,
                            Language = LanguageContext.CurrentLanguage,
                            Priority = int.MaxValue
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
                    IsActive = model.IsActive,
                    Language = LanguageContext.CurrentLanguage,
                    Priority = model.Priority
                };
                productService.Add(product);
                productService.Save();
            }
            return RedirectToAction("ProductAdd", "Pages", new { area = "Admin" });
        }

        [AdminAuthorize]
        public IActionResult ProductEditMode(int id)
        {

            Product product = productService.Get(x => x.id == id);
            if (product != null)
            {
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
                    PhotoPath = product.PhotoPath,
                    Priority = product.Priority
                });
            }

            return View("ProductAdd", new CategoryViewModel());

        }

        [AdminAuthorize]
        [HttpPost]
        public IActionResult ProductEditMode(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product findedProduct = null;
                Category category = categoryService.Get(x => x.id == model.Categoryid && x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
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
                    findedProduct.Language = LanguageContext.CurrentLanguage;
                    findedProduct.Priority = model.Priority;
                    if (category != null)
                    {
                        findedProduct.Categoryid = category.id;
                    }

                    if (!string.IsNullOrWhiteSpace(uniqueFileName))
                    {
                        findedProduct.PhotoPath = uniqueFileName;
                    }

                    productService.Update(findedProduct);
                    productService.Save();
                }

            }
            return RedirectToAction("ProductAdd", "Pages", new { area = "Admin" });
        }
        [AdminAuthorize]
        public IActionResult ProductDataTransferPage()
        {
            return View();
        }
        [AdminAuthorize]
        public JsonResult ProductDataTransferAjax(int leftId, int rightId)
        {
            if (leftId != null && rightId != null)
            {
                ProductDataTransfer(leftId, rightId);
                return Json("Başarılı");
            }
            return Json("Basarisiz");
        }

        public void ProductDataTransfer(int leftId, int rightId)
        {
            Product left = productService.GetAllLanguageProduct(x => x.id == leftId);
            Product right = productService.GetAllLanguageProduct(x => x.id == rightId);
            if (left != null && right != null)
            {
                typeof(Product).GetProperties().Where(x => x.Name != "id").ToList().ForEach(item => item.SetValue(right, item.GetValue(left)));
                productService.Update(right);
                productService.Save();
            }

        }

        [AdminAuthorize]
        [HttpGet]
        [Route("Urun-Silme-{id:int}")]
        public IActionResult ProductDelete(int id)
        {
            Product product = productService.Get(x => x.id == id);
            Category category = null;
            if (product != null)
            {
                category = categoryService.Get(x => x.id == product.Categoryid && x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
            }

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
                    WebSite = Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite,
                    Language = LanguageContext.CurrentLanguage,
                    Priority = model.Priority
                };
                Blog findedBlog = blogService.Get(x => x.Title.Equals(blog.Title) && x.WebSite == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite);
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
                    findedBlog.Language = LanguageContext.CurrentLanguage;
                    findedBlog.Priority = blog.Priority;
                    if (!string.IsNullOrWhiteSpace(blog.PhotoPath))
                    {
                        findedBlog.PhotoPath = blog.PhotoPath;
                    }

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
            Blog findedBlog = blogService.Get(m => m.id == id);
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
                Blog findedBlog = blogService.Get(m => m.id == id);

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
                    findedBlog.Language = LanguageContext.CurrentLanguage;
                    findedBlog.Priority = model.Priority;
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
                        IsActive = model.IsActive,
                        Language = LanguageContext.CurrentLanguage,
                        Priority = model.Priority
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
            System.Collections.Generic.List<Blog> data = blogService.GetMany(x => x.WebSiteid == WebsiteContext.CurrentWebsite.id && x.IsActive == true).OrderByDescending(x => x.Priority).ToList();
            return View(data);
        }

        #endregion
        #region Category
        [AdminAuthorize]
        [HttpGet]
        [Route("Kategori-Olustur")]
        public IActionResult CategoryAdd()
        {

            return View(new CategoryViewModel());
        }

        [AdminAuthorize]
        [HttpGet]
        public IActionResult CategoryEditMode(int id)
        {

            Category category = categoryService.Get(x => x.id == id && x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
            if (category != null)
            {
                return View("CategoryAdd", new CategoryViewModel() { ParentId = category.Parentid, Name = category.Name, ImagePath = category.PhotoPath });
            }

            return View("CategoryAdd", new CategoryViewModel());

        }
        [AdminAuthorize]
        [HttpPost]
        public IActionResult CategoryEditMode(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category findedCategory = categoryService.Get(x => x.id == model.id && x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);

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
                    findedCategory.Priority = model.Priority;
                    if (!string.IsNullOrWhiteSpace(uniqueFileName))
                    {
                        findedCategory.PhotoPath = uniqueFileName;
                    }

                    findedCategory.Language = LanguageContext.CurrentLanguage;
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
                    WebSite = Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite,
                    CreateUserid = LoginUser.id,
                    Language = LanguageContext.CurrentLanguage,
                    Priority = model.Priority
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
            Category category = categoryService.Get(x => x.id == id && Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id == x.WebSiteid);
            if (category != null)
            {
                categoryService.Delete(category);
                categoryService.Save();
            }
            return RedirectToAction("CategoryAdd", "Pages", new { area = "Admin" });
        }

        #endregion
        #region Language
        [AdminAuthorize]
        [Route("Dil-Olustur")]
        public IActionResult LanguageAdd()
        {
            return View(new LanguageViewModel());
        }
        [AdminAuthorize]
        [HttpPost]
        [Route("Dil-Olustur")]
        public IActionResult LanguageAdd(LanguageViewModel model)
        {

            if (ModelState.IsValid)
            {
                Language language = languageService.Get(x => x.IsActive == true && x.Name.Equals(model.Name) && x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
                if (language == null)
                {
                    bool langdef = languageService.Get(x => x.IsActive == true && x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id && x.Default == true) == null ? true : false;
                    string uniqueFileName = null;
                    if (model.IconCode != null)
                    {
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "LanguageImages");
                        string Extension = Path.GetExtension(model.IconCode.FileName);
                        uniqueFileName = $"{model.KeyCode}_{model.Name}{Extension}";
                        uniqueFileName = Bll.Helpers.FriendlyUrlHelper.CleanFileName(uniqueFileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        if (!System.IO.File.Exists(filePath))
                        {
                            model.IconCode.CopyTo(new FileStream(filePath, FileMode.Create));
                        }
                    }
                    language = new Language()
                    {
                        Name = model.Name,
                        KeyCode = model.KeyCode,
                        IconCode = uniqueFileName,
                        Default = langdef,
                        CreateUserid = LoginUser.id,
                        WebSite = Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite
                    };
                    languageService.Add(language);
                    languageService.Save();
                }
            }

            return View(model);
        }
        #endregion
        #region StaticPages 
        [AdminAuthorize]
        [HttpGet]
        public IActionResult NavbarHeaderAdd()
        {

            UI_Navbar navbar;
            navbar = uI_NavbarService.Get(x => x.IsActive == true && x.WebSiteid == WebsiteContext.CurrentWebsite.id);
            if (navbar == null)
            {
                navbar = new UI_Navbar();
            }

            return View(navbar);
        }
        [AdminAuthorize]
        [HttpPost]
        public IActionResult NavbarHeaderAdd(UI_Navbar model)
        {

            UI_Navbar navbar;
            navbar = uI_NavbarService.Get(x => x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
            if (navbar == null)
            {
                navbar = model;
                navbar.CreateUserid = LoginUser.id;
                navbar.WebSite = Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite;
                navbar.Language = LanguageContext.CurrentLanguage;
                uI_NavbarService.Add(navbar);
            }
            else
            {
                navbar.Home = model.Home;
                navbar.Products = model.Products;
                navbar.AboutUs = model.AboutUs;
                navbar.PetroBlog = model.PetroBlog;
                navbar.Contact = model.Contact;
                navbar.UpdateUserid = LoginUser.id;
                navbar.UpdateDate = DateTime.UtcNow;
                navbar.Language = LanguageContext.CurrentLanguage;
                navbar.Languages = model.Languages;
                uI_NavbarService.Update(navbar);
            }
            uI_NavbarService.Save();
            return View(navbar);
        }

        [AdminAuthorize]
        [HttpGet]
        public IActionResult FooterAdd()
        {
            UI_Footer footer;
            footer = uI_FooterService.Get(x => x.IsActive == true && x.WebSiteid == WebsiteContext.CurrentWebsite.id);
            if (footer == null)
            {
                footer = new UI_Footer();
            }

            return View(footer);
        }
        [AdminAuthorize]
        [HttpPost]
        public IActionResult FooterAdd(UI_Footer model)
        {
            UI_Footer footer;
            footer = uI_FooterService.Get(x => x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
            if (footer == null)
            {
                footer = model;
                footer.CreateUserid = LoginUser.id;
                footer.WebSite = Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite;
                footer.Language = LanguageContext.CurrentLanguage;
                uI_FooterService.Add(footer);
            }
            else
            {
                footer.Content = model.Content;

                footer.UpdateUserid = LoginUser.id;
                footer.UpdateDate = DateTime.UtcNow;
                footer.Language = LanguageContext.CurrentLanguage;
                uI_FooterService.Update(footer);
            }
            uI_FooterService.Save();
            return View(footer);
        }
        [AdminAuthorize]
        [HttpGet]
        public IActionResult ContactAdd()
        {
            UI_Contact contact;
            contact = uI_ContactService.Get(x => x.IsActive == true && x.WebSiteid == WebsiteContext.CurrentWebsite.id);
            if (contact == null)
            {
                contact = new UI_Contact();
            }

            return View(contact);
        }
        [AdminAuthorize]
        [HttpPost]
        public IActionResult ContactAdd(UI_Contact model)
        {
            UI_Contact contact;
            contact = uI_ContactService.Get(x => x.WebSiteid == Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite.id);
            if (contact == null)
            {
                contact = model;
                contact.CreateUserid = LoginUser.id;
                contact.WebSite = Petroteks.Bll.Helpers.WebsiteContext.CurrentWebsite;
                contact.Language = LanguageContext.CurrentLanguage;
                uI_ContactService.Add(contact);
            }
            else
            {
                contact.Content = model.Content;

                contact.UpdateUserid = LoginUser.id;
                contact.UpdateDate = DateTime.UtcNow;
                contact.Language = LanguageContext.CurrentLanguage;
                uI_ContactService.Update(contact);
            }
            uI_ContactService.Save();
            return View(contact);
        }
        #endregion
    }
}