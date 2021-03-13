using ImageMagick;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Petroteks.Bll.Abstract;
using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Areas.Admin.Models;
using Petroteks.MvcUi.Attributes;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Services;
using Petroteks.MvcUi.StringInfos;

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
        private readonly ILanguageService languageService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IUI_ContactService uI_ContactService;
        private readonly ImageOptimizer imageOptimizer;
        private readonly ILogger<ImageOptimizer> logger;
        private readonly ICacheService cacheService;
        #endregion
        #region CTOR

        public PagesController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            mainPageService = serviceProvider.GetService<IMainPageService>();
            aboutUsObjectService = serviceProvider.GetService<IAboutUsObjectService>();
            privacyPolicyObjectService = serviceProvider.GetService<IPrivacyPolicyObjectService>();
            categoryService = serviceProvider.GetService<ICategoryService>();
            productService = serviceProvider.GetService<IProductService>();
            blogService = serviceProvider.GetService<IBlogService>();
            uI_NavbarService = serviceProvider.GetService<IUI_NavbarService>();
            uI_FooterService = serviceProvider.GetService<IUI_FooterService>();
            languageService = serviceProvider.GetService<ILanguageService>();
            uI_ContactService = serviceProvider.GetService<IUI_ContactService>();
            hostingEnvironment = serviceProvider.GetService<IWebHostEnvironment>();
            cacheService = serviceProvider.GetService<ICacheService>();
            imageOptimizer = serviceProvider.GetService<ImageOptimizer>();
            logger = serviceProvider.GetService<ILogger<ImageOptimizer>>();
        }
        #endregion
        #region Pages
        [AdminAuthorize]
        [Route("Anasayfa-Duzenleme")]
        public IActionResult AnaSayfaEdit()
        {
            MainPage mainPage;
            mainPage = mainPageService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
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
            mainPage = mainPageService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            if (mainPage == null)
            {
                mainPage = model;
                mainPage.CreateUserid = LoginUser.id;
                mainPage.WebSite = CurrentWebsite;
                mainPage.Language = CurrentLanguage;
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
                mainPage.Language = CurrentLanguage;
                mainPageService.Update(mainPage);
            }
            mainPageService.Save();
            cacheService.Remove($"{CacheInfo.MainPage}-{CurrentWebsite.id}-{CurrentLanguage.id}");
            return View(mainPage);
        }


        [AdminAuthorize]
        [Route("Hakkimizda-Duzenleme")]
        public IActionResult HakkimizdaEdit()
        {
            AboutUsObject aboutus;
            aboutus = aboutUsObjectService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
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
            aboutus = aboutUsObjectService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            if (aboutus == null)
            {
                aboutus = model;
                aboutus.WebSite = CurrentWebsite;
                aboutus.CreateUserid = LoginUser.id;
                aboutus.Language = CurrentLanguage;
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
                aboutus.Language = CurrentLanguage;
                aboutUsObjectService.Update(aboutus);
            }
            aboutUsObjectService.Save();
            cacheService.Remove($"{CacheInfo.AboutUs}-{CurrentWebsite.id}-{CurrentLanguage.id}");
            return View(aboutus);
        }


        [AdminAuthorize]
        [Route("Gizlilik-Politikasi-Duzenleme")]
        public IActionResult GizlilikPolitikasiEdit()
        {
            PrivacyPolicyObject privacyPage;
            privacyPage = privacyPolicyObjectService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
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
            privacyPage = privacyPolicyObjectService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            if (privacyPage == null)
            {
                privacyPage = model;
                privacyPage.WebSite = CurrentWebsite;
                privacyPage.CreateUserid = LoginUser.id;
                privacyPage.Language = CurrentLanguage;
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
                privacyPage.Language = CurrentLanguage;
                privacyPolicyObjectService.Update(privacyPage);
            }
            privacyPolicyObjectService.Save();
            cacheService.Remove($"{CacheInfo.PrivacyPolicy}-{CurrentWebsite.id}-{CurrentLanguage.id}");
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
                Category category;
                category = categoryService.Get(x => x.id == model.Categoryid, CurrentLanguage.id);
                if (category == null && model.Categoryid == 0)
                {
                    Category rootCategory = categoryService.Get(x => x.Name == "ROOT" && x.WebSite.id == CurrentWebsite.id, CurrentLanguage.id);
                    if (rootCategory == null)
                    {
                        rootCategory = new Category()
                        {
                            Name = "ROOT",
                            Parentid = 0,
                            WebSite = CurrentWebsite,
                            Language = CurrentLanguage,
                            Priority = int.MaxValue
                        };
                        categoryService.Add(rootCategory);
                        categoryService.Save();
                    }
                    category = rootCategory;
                }
                else if(category==null && model.Categoryid!=0)
                {
                    ModelState.AddModelError("","Girdiğiz kategori kimliği ile bir kategori bulunamadı");
                    return View(model);
                }

                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "ProductImages");
                    uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    imageOptimizer.Optimize(filePath, logger);
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
                    Language = CurrentLanguage,
                    Priority = model.Priority
                };
                productService.Add(product);
                productService.Save();
                cacheService.Remove($"{CacheInfo.SubProducts}-{category.id}-{category.Languageid.Value}");
                cacheService.Remove($"{CacheInfo.OrderedProducts}-{CurrentWebsite.id}-{CurrentLanguage.id}");
            }
            return RedirectToAction("ProductAdd", "Pages", new { area = "Admin" });
        }

        [AdminAuthorize]
        public IActionResult ProductEditMode(int id)
        {

            Product product = productService.Get(x => x.id == id, CurrentLanguage.id);
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
                Category category = categoryService.Get(x => x.id == model.Categoryid && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
                findedProduct = productService.Get(x => x.id == model.id, CurrentLanguage.id);
                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "ProductImages");
                    uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    imageOptimizer.Optimize(filePath, logger);
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
                    findedProduct.Language = CurrentLanguage;
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
                    cacheService.Remove($"{CacheInfo.SubProducts}-{category.id}-{category.Languageid.Value}");
                    cacheService.Remove($"{CacheInfo.OrderedProducts}-{CurrentWebsite.id}-{CurrentLanguage.id}");
                    cacheService.Remove($"{CacheInfo.Product}-{findedProduct.id}");
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
                var lang = right.Category?.Languageid.Value ?? CurrentLanguageId;
                cacheService.Remove($"{CacheInfo.SubProducts}-{right.Categoryid}-{lang}");
                cacheService.Remove($"{CacheInfo.OrderedProducts}-{CurrentWebsite.id}-{lang}");
                cacheService.Remove($"{CacheInfo.Product}-{right.id}");
            }

        }

        [AdminAuthorize]
        [HttpGet]
        [Route("Urun-Silme-{id:int}")]
        public IActionResult ProductDelete(int id)
        {
            Product product = productService.Get(x => x.id == id, CurrentLanguage.id);
            Category category = null;
            if (product != null)
            {
                category = categoryService.Get(x => x.id == product.Categoryid && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            }

            if (category != null && product != null)
            {
                productService.Delete(product);
                productService.Save();
                cacheService.Remove($"{CacheInfo.SubProducts}-{category.id}-{category.Languageid.Value}");
                cacheService.Remove($"{CacheInfo.OrderedProducts}-{CurrentWebsite.id}-{CurrentLanguage.id}");
                cacheService.Remove($"{CacheInfo.Product}-{product.id}");
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
                    imageOptimizer.Optimize(filePath, logger);
                }
                Blog findedBlog = blogService.Get(x => x.Title.Equals(model.Title) && x.WebSite == CurrentWebsite, CurrentLanguage.id);
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
                    findedBlog.Language = CurrentLanguage;
                    findedBlog.Name = model.Name;
                    findedBlog.WebSite = CurrentWebsite;
                    findedBlog.Priority = model.Priority;
                    if (!string.IsNullOrWhiteSpace(uniqueFileName))
                        findedBlog.PhotoPath = uniqueFileName;
                    blogService.Update(findedBlog);
                }
                else
                {
                    findedBlog = new Blog()
                    {
                        Title = model.Title,
                        Name=model.Name,
                        PhotoPath = uniqueFileName,
                        Description = model.Description,
                        MetaTags = model.MetaTags,
                        Keywords = model.Keywords,
                        Content = model.Content,
                        CreateUserid = LoginUser.id,
                        CreateDate = DateTime.UtcNow,
                        IsActive = model.IsActive,
                        WebSite = CurrentWebsite,
                        Language = CurrentLanguage,
                        Priority = model.Priority
                    };
                    blogService.Add(findedBlog);
                }
                blogService.Save();
                cacheService.Remove($"{CacheInfo.OrderedBlogs}-{CurrentWebsite.id}-{CurrentLanguage.id}");
                cacheService.Remove($"{CacheInfo.Blog}-{findedBlog.id}-{CurrentWebsite.id}");
            }
            return RedirectToAction("BlogAdd", "Pages", new { area = "Admin" });
        }
        [AdminAuthorize]
        [Route("Blog-Duzenleme-{id:int}")]
        public IActionResult BlogEdit(int id)
        {
            Blog findedBlog = blogService.Get(m => m.id == id, CurrentLanguage.id);
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
            Blog blog = blogService.Get(x => x.id == id, CurrentLanguage.id);
            if (blog != null)
            {
                blog.IsActive = false;
                blogService.Save();
                cacheService.Remove($"{CacheInfo.OrderedBlogs}-{CurrentWebsite.id}-{CurrentLanguage.id}");
                cacheService.Remove($"{CacheInfo.Blog}-{blog.id}-{CurrentWebsite.id}");
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
                string uniqueFileName = null;
                if (model.PhotoPath != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "BlogImages");
                    uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + model.PhotoPath.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.PhotoPath.CopyTo(new FileStream(filePath, FileMode.Create));
                    imageOptimizer.Optimize(filePath, logger);
                }
                Blog findedBlog = blogService.Get(m => m.id == id, CurrentLanguage.id);
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
                    findedBlog.Name = model.Name;
                    findedBlog.Language = CurrentLanguage;
                    findedBlog.WebSite = CurrentWebsite;
                    findedBlog.Priority = model.Priority;
                    if (!string.IsNullOrWhiteSpace(uniqueFileName))
                        findedBlog.PhotoPath = uniqueFileName;
                    blogService.Update(findedBlog);
                }
                else
                {
                    findedBlog = new Blog()
                    {
                        Title = model.Title,
                        Name=model.Name,
                        PhotoPath = uniqueFileName,
                        Description = model.Description,
                        MetaTags = model.MetaTags,
                        Keywords = model.Keywords,
                        Content = model.Content,
                        CreateUserid = LoginUser.id,
                        CreateDate = DateTime.UtcNow,
                        IsActive = model.IsActive,
                        WebSite = CurrentWebsite,
                        Language = CurrentLanguage,
                        Priority = model.Priority
                    };
                    blogService.Add(findedBlog);
                }
                blogService.Save();
                cacheService.Remove($"{CacheInfo.OrderedBlogs}-{CurrentWebsite.id}-{CurrentLanguage.id}");
                cacheService.Remove($"{CacheInfo.Blog}-{findedBlog.id}-{CurrentWebsite.id}");
            }

            return RedirectToAction("BlogList", "Pages", new { area = "Admin" });
        }
        [AdminAuthorize]
        [Route("Admin-Panel/Bloglar")]
        public IActionResult BlogList()
        {
            System.Collections.Generic.List<Blog> data = blogService.GetMany(x => x.WebSiteid == CurrentWebsite.id && x.IsActive == true, CurrentLanguage.id).OrderByDescending(x => x.Priority).ToList();
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
                    imageOptimizer.Optimize(filePath, logger);
                }
                Category category = new Category()
                {
                    Name = model.Name,
                    Content = model.Content,
                    Description = model.Description,
                    Keywords = model.Keywords,
                    MetaTags = model.MetaTags,
                    Parentid = model.ParentId,
                    PhotoPath = uniqueFileName,
                    WebSite = CurrentWebsite,
                    CreateUserid = LoginUser.id,
                    Language = CurrentLanguage,
                    Priority = model.Priority
                };
                categoryService.Add(category);
                categoryService.Save();
                cacheService.Remove($"{CacheInfo.OrderedCategories}-{CurrentWebsite.id}-{CurrentLanguage.id}");
            }
            return RedirectToAction("CategoryAdd", "Pages", new { area = "Admin" });
        }

        [AdminAuthorize]
        [HttpGet]
        public IActionResult CategoryEditMode(int id)
        {

            Category category = categoryService.Get(x => x.id == id && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            if (category != null)
            {
                return View("CategoryAdd", new CategoryViewModel() { ParentId = category.Parentid, Name = category.Name, ImagePath = category.PhotoPath, Content = category.Content, Description = category.Description, Keywords = category.Keywords, MetaTags = category.MetaTags });
            }
            return View("CategoryAdd", new CategoryViewModel());

        }
        [AdminAuthorize]
        [HttpPost]
        public IActionResult CategoryEditMode(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category findedCategory = categoryService.Get(x => x.id == model.id && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);

                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "CategoryImages");
                    uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    imageOptimizer.Optimize(filePath, logger);
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
                    findedCategory.Description = model.Description;
                    findedCategory.Content = model.Content;
                    findedCategory.Keywords = model.Keywords;
                    findedCategory.MetaTags = model.MetaTags;
                    findedCategory.Language = CurrentLanguage;
                    categoryService.Update(findedCategory);
                    categoryService.Save();
                    cacheService.Remove($"{CacheInfo.OrderedCategories}-{CurrentWebsite.id}-{CurrentLanguage.id}");
                    cacheService.Remove($"{CacheInfo.Category}-{findedCategory.id}-{CurrentWebsite.id}");
                }

            }
            return RedirectToAction("CategoryAdd", "Pages", new { area = "Admin" });
        }


        [AdminAuthorize]
        [HttpGet]
        [Route("Kategori-Silme-{id:int}")]
        public IActionResult CategoryDelete(int id)
        {
            Category category = categoryService.Get(x => x.id == id && CurrentWebsite.id == x.WebSiteid, CurrentLanguage.id);
            if (category != null)
            {
                categoryService.Delete(category);
                categoryService.Save();
                cacheService.Remove($"{CacheInfo.OrderedCategories}-{CurrentWebsite.id}-{CurrentLanguage.id}");
                cacheService.Remove($"{CacheInfo.Category}-{category.id}-{CurrentWebsite.id}");
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
                Language language = languageService.Get(x => x.IsActive == true && x.Name.Equals(model.Name) && x.WebSiteid == CurrentWebsite.id);
                if (language == null)
                {
                    bool langdef = languageService.Get(x => x.IsActive == true && x.WebSiteid == CurrentWebsite.id && x.Default == true) == null ? true : false;
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
                            imageOptimizer.Optimize(filePath, logger);
                        }
                    }
                    language = new Language()
                    {
                        Name = model.Name,
                        KeyCode = model.KeyCode,
                        IconCode = uniqueFileName,
                        Default = langdef,
                        CreateUserid = LoginUser.id,
                        WebSite = CurrentWebsite
                    };
                    languageService.Add(language);
                    languageService.Save();
                    cacheService.Remove($"{CacheInfo.Languages}");
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
            navbar = uI_NavbarService.Get(x => x.IsActive == true && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
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
            navbar = uI_NavbarService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            if (navbar == null)
            {
                navbar = model;
                navbar.CreateUserid = LoginUser.id;
                navbar.WebSite = CurrentWebsite;
                navbar.Language = CurrentLanguage;
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
                navbar.Language = CurrentLanguage;
                navbar.Languages = model.Languages;
                uI_NavbarService.Update(navbar);
            }
            uI_NavbarService.Save();
            cacheService.Remove($"{CacheInfo.Navbar}-{CurrentWebsite.id}-{CurrentLanguage.id}");

            return View(navbar);
        }

        [AdminAuthorize]
        [HttpGet]
        public IActionResult FooterAdd()
        {
            UI_Footer footer;
            footer = uI_FooterService.Get(x => x.IsActive == true && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
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
            footer = uI_FooterService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            if (footer == null)
            {
                footer = model;
                footer.CreateUserid = LoginUser.id;
                footer.WebSite = CurrentWebsite;
                footer.Language = CurrentLanguage;
                uI_FooterService.Add(footer);
            }
            else
            {
                footer.Content = model.Content;

                footer.UpdateUserid = LoginUser.id;
                footer.UpdateDate = DateTime.UtcNow;
                footer.Language = CurrentLanguage;
                uI_FooterService.Update(footer);
            }
            uI_FooterService.Save();
            cacheService.Remove($"{CacheInfo.Footer}-{CurrentWebsite.id}-{CurrentLanguage.id}");

            return View(footer);
        }
        [AdminAuthorize]
        [HttpGet]
        public IActionResult ContactAdd()
        {
            UI_Contact contact;
            contact = uI_ContactService.Get(x => x.IsActive == true && x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
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
            contact = uI_ContactService.Get(x => x.WebSiteid == CurrentWebsite.id, CurrentLanguage.id);
            if (contact == null)
            {
                contact = model;
                contact.CreateUserid = LoginUser.id;
                contact.WebSite = CurrentWebsite;
                contact.Language = CurrentLanguage;
                uI_ContactService.Add(contact);
            }
            else
            {
                contact.Content = model.Content;

                contact.UpdateUserid = LoginUser.id;
                contact.UpdateDate = DateTime.UtcNow;
                contact.Language = CurrentLanguage;
                uI_ContactService.Update(contact);
            }
            uI_ContactService.Save();
            cacheService.Remove($"{CacheInfo.Contact}-{CurrentWebsite.id}-{CurrentLanguage.id}");

            return View(contact);
        }
        #endregion
    }
}