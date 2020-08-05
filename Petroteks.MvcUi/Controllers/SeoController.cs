using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Concreate;
using Petroteks.Bll.Helpers;
using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Models;
using static Petroteks.Bll.Helpers.FriendlyUrlHelper;

namespace Petroteks.MvcUi.Controllers
{
    public class SeoController : GlobalController
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IDynamicPageService dynamicPageService;
        private readonly IMainPageService mainPageService;
        private readonly IAboutUsObjectService aboutUsObjectService;
        private readonly IPrivacyPolicyObjectService privacyPolicyObjectService;
        private readonly IUI_ContactService uI_ContactService;
        private readonly IBlogService blogService;
        private readonly IML_ProductService mL_ProductService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IRouteTable routeTable;


        public SeoController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            categoryService = serviceProvider.GetService<ICategoryService>();
            productService = serviceProvider.GetService<IProductService>();
            dynamicPageService = serviceProvider.GetService<IDynamicPageService>();
            mainPageService = serviceProvider.GetService<IMainPageService>();
            aboutUsObjectService = serviceProvider.GetService<IAboutUsObjectService>();
            uI_ContactService = serviceProvider.GetService<IUI_ContactService>();
            blogService = serviceProvider.GetService<IBlogService>();
            privacyPolicyObjectService = serviceProvider.GetService<IPrivacyPolicyObjectService>();
            mL_ProductService = serviceProvider.GetService<IML_ProductService>();
            webHostEnvironment = serviceProvider.GetService<IWebHostEnvironment>();
            routeTable = serviceProvider.GetService<IRouteTable>();
        }

        #region Eski Hal
        //[Route("sitemap.xml")]
        //public IActionResult SitemapXml()
        //{
        //    IActionResult staticfileload()
        //    {
        //        try
        //        {
        //            string sitemapxml = Path.Combine(Path.Combine(webHostEnvironment.WebRootPath, "Temp"), "sitemap.xml");
        //            return Content(System.IO.File.ReadAllText(sitemapxml), "text/xml");
        //        }
        //        catch (Exception ex)
        //        {
        //            return Content($"Hata {ex.Message}");
        //        }
        //    }
        //    Response.Clear();
        //    Response.ContentType = "text/xml";
        //    StringWriter sw = new StringWriter();
        //    if (CurrentWebsite != null && CurrentLanguage != null)
        //    {
        //        try
        //        {
        //            XmlTextWriter xtr = new XmlTextWriter(sw);
        //            xtr.WriteStartDocument();
        //            xtr.WriteStartElement("urlset");
        //            xtr.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
        //            xtr.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        //            xtr.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");

        //            string siteUrl = CurrentWebsite.BaseUrl.Replace("www.", "", System.StringComparison.CurrentCultureIgnoreCase);

        //            MainPage mainPage = mainPageService.Get(x => x.WebSiteid == CurrentWebsite.id && x.IsActive, CurrentLanguage.id);
        //            if (mainPage != null)
        //            {
        //                xtr.WriteStartElement("url");
        //                xtr.WriteElementString("loc", $"{siteUrl}");
        //                xtr.WriteElementString("lastmod", $"{(mainPage.UpdateDate ?? mainPage.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
        //                xtr.WriteEndElement();

        //            }


        //            AboutUsObject aboutUs = aboutUsObjectService.Get(x => x.WebSiteid == CurrentWebsite.id && x.IsActive, CurrentLanguage.id);
        //            if (aboutUs != null)
        //            {
        //                xtr.WriteStartElement("url");
        //                xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("AboutUs", "Home")}");
        //                xtr.WriteElementString("lastmod", $"{(aboutUs.UpdateDate ?? aboutUs.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
        //                xtr.WriteEndElement();
        //            }

        //            PrivacyPolicyObject privacyPolicyObject = privacyPolicyObjectService.Get(x => x.WebSiteid == CurrentWebsite.id && x.IsActive, CurrentLanguage.id);
        //            if (privacyPolicyObject != null)
        //            {
        //                xtr.WriteStartElement("url");
        //                xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("PrivacyPolicy", "Home")}");
        //                xtr.WriteElementString("lastmod", $"{(privacyPolicyObject.UpdateDate ?? privacyPolicyObject.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
        //                xtr.WriteEndElement();
        //            }


        //            UI_Contact contact = uI_ContactService.Get(x => x.WebSiteid == CurrentWebsite.id && x.IsActive, CurrentLanguage.id);
        //            if (contact != null)
        //            {
        //                xtr.WriteStartElement("url");
        //                xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("Contact", "Home")}");
        //                xtr.WriteElementString("lastmod", $"{(contact.UpdateDate ?? contact.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
        //                xtr.WriteEndElement();
        //            }



        //            if (siteUrl.Equals("https://petroteks.com"))
        //            {
        //                xtr.WriteStartElement("url");
        //                xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("SondajKopugu", "Home")}");
        //                xtr.WriteEndElement();


        //                xtr.WriteStartElement("url");
        //                xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("PetroBlog", "Home")}");
        //                xtr.WriteEndElement();

        //            }

        //            foreach (Language lang in LanguageContext.WebsiteLanguages)
        //            {
        //                ICollection<Category> Categories = new List<Category>();
        //                try
        //                {
        //                    Categories = categoryService.GetMany(x => x.WebSiteid == CurrentWebsite.id && x.IsActive == true, lang.id);

        //                    foreach (Category item in Categories)
        //                    {
        //                        try
        //                        {
        //                            xtr.WriteStartElement("url");
        //                            xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("CategoryDetail", "Detail", new { categoryName = GetFriendlyTitle(item.Name), page = 1, category = item.id })}");
        //                            xtr.WriteElementString("lastmod", $"{(item.UpdateDate ?? item.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
        //                            xtr.WriteEndElement();
        //                        }
        //                        catch
        //                        {
        //                            xtr.WriteEndElement();
        //                        }
        //                    }
        //                }
        //                catch { }

        //                try
        //                {

        //                    ICollection<Product> Products = productService.GetMany(x => x.IsActive == true, lang.id);

        //                    var WebsiteProducts =
        //                                        from category in Categories
        //                                        join prod in Products on category.id equals prod.Categoryid
        //                                        select new { ProductName = prod.SupTitle, id = prod.id, UpdateDate = prod.UpdateDate, CreateDate = prod.CreateDate };


        //                    foreach (var item in WebsiteProducts)
        //                    {
        //                        try
        //                        {

        //                            xtr.WriteStartElement("url");
        //                            xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("ProductDetail", "Detail", new { produtname = GetFriendlyTitle(item.ProductName), id = item.id })}");
        //                            xtr.WriteElementString("lastmod", $"{(item.UpdateDate ?? item.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
        //                            xtr.WriteEndElement();
        //                        }
        //                        catch
        //                        {
        //                            xtr.WriteEndElement();
        //                        }
        //                    }
        //                }
        //                catch { }

        //                try
        //                {
        //                    ICollection<DynamicPage> dynamicPages = dynamicPageService.GetMany(x => x.WebSiteid == CurrentWebsite.id && x.IsActive == true, lang.id);

        //                    foreach (DynamicPage item in dynamicPages)
        //                    {
        //                        try
        //                        {
        //                            xtr.WriteStartElement("url");
        //                            xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("DynamicPageView", "Home", new { pageName = GetFriendlyTitle(item.Name), id = item.id })}");
        //                            xtr.WriteElementString("lastmod", $"{(item.UpdateDate ?? item.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
        //                            xtr.WriteEndElement();
        //                        }
        //                        catch
        //                        {
        //                            xtr.WriteEndElement();
        //                        }
        //                    }
        //                }
        //                catch { }

        //                try
        //                {
        //                    ICollection<Blog> blogs = blogService.GetMany(x => x.WebSiteid == CurrentWebsite.id && x.IsActive == true, lang.id);

        //                    foreach (Blog item in blogs)
        //                    {
        //                        try
        //                        {
        //                            xtr.WriteStartElement("url");
        //                            xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("BlogDetail", "Home", new { title = GetFriendlyTitle(item.Title), id = item.id })}");
        //                            xtr.WriteElementString("lastmod", $"{(item.UpdateDate ?? item.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
        //                            xtr.WriteEndElement();
        //                        }
        //                        catch
        //                        {
        //                            xtr.WriteEndElement();
        //                        }

        //                    }
        //                }
        //                catch { }
        //            }

        //            try
        //            {
        //                xtr.WriteEndElement();
        //                xtr.WriteEndDocument();
        //                xtr.Flush();
        //                xtr.Close();
        //            }
        //            catch
        //            {
        //                return staticfileload();
        //            }
        //        }
        //        catch
        //        {
        //            return staticfileload();
        //        }

        //        try
        //        {
        //            using (StreamWriter streamWriter = new StreamWriter(Response.Body))
        //            {
        //                streamWriter.Write(sw.ToString());
        //            }
        //        }
        //        catch
        //        {
        //            return staticfileload();
        //        }
        //    }
        //    else
        //    {
        //        return staticfileload();
        //    }
        //    return View();
        //} 
        #endregion
        [Route("sitemap.xml")]
        public IActionResult SitemapXml()
        {
            IActionResult staticfileload()
            {
                try
                {
                    string sitemapxml = Path.Combine(Path.Combine(webHostEnvironment.WebRootPath, "Temp"), "sitemap.xml");
                    return Content(System.IO.File.ReadAllText(sitemapxml), "text/xml");
                }
                catch (Exception ex)
                {
                    return Content($"Hata {ex.Message}");
                }
            }
            Response.Clear();
            Response.ContentType = "text/xml";
            StringWriter sw = new StringWriter();
            if (CurrentWebsite != null && CurrentLanguage != null)
            {
                try
                {
                    XmlTextWriter xtr = new XmlTextWriter(sw);
                    xtr.WriteStartDocument();
                    xtr.Formatting = Formatting.Indented;
                    xtr.WriteStartElement("urlset");
                    xtr.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                    xtr.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    xtr.WriteAttributeString("xmlns:xhtml", "http://www.w3.org/1999/xhtml");
                    xtr.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd http://www.w3.org/1999/xhtml http://www.w3.org/2002/08/xhtml/xhtml1-strict.xsd");

                    string siteUrl = CurrentWebsite.BaseUrl.Replace("www.", "", System.StringComparison.CurrentCultureIgnoreCase);

                    MainPage mainPage = mainPageService.Get(x => x.WebSiteid == CurrentWebsite.id && x.IsActive, CurrentLanguage.id);
                    if (mainPage != null)
                    {
                        xtr.WriteStartElement("url");
                        xtr.WriteElementString("loc", $"{siteUrl}");
                        xtr.WriteElementString("lastmod", $"{(mainPage.UpdateDate ?? mainPage.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
                        xtr.WriteEndElement();

                    }


                    AboutUsObject aboutUs = aboutUsObjectService.Get(x => x.WebSiteid == CurrentWebsite.id && x.IsActive, CurrentLanguage.id);
                    if (aboutUs != null)
                    {
                        xtr.WriteStartElement("url");
                        xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("AboutUs", "Home")}");
                        xtr.WriteElementString("lastmod", $"{(aboutUs.UpdateDate ?? aboutUs.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
                        xtr.WriteEndElement();
                    }

                    PrivacyPolicyObject privacyPolicyObject = privacyPolicyObjectService.Get(x => x.WebSiteid == CurrentWebsite.id && x.IsActive, CurrentLanguage.id);
                    if (privacyPolicyObject != null)
                    {
                        xtr.WriteStartElement("url");
                        xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("PrivacyPolicy", "Home")}");
                        xtr.WriteElementString("lastmod", $"{(privacyPolicyObject.UpdateDate ?? privacyPolicyObject.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
                        xtr.WriteEndElement();
                    }


                    UI_Contact contact = uI_ContactService.Get(x => x.WebSiteid == CurrentWebsite.id && x.IsActive, CurrentLanguage.id);
                    if (contact != null)
                    {
                        xtr.WriteStartElement("url");
                        xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("Contact", "Home")}");
                        xtr.WriteElementString("lastmod", $"{(contact.UpdateDate ?? contact.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
                        xtr.WriteEndElement();
                    }



                    if (siteUrl.Equals("https://petroteks.com"))
                    {
                        xtr.WriteStartElement("url");
                        xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("SondajKopugu", "Home")}");
                        xtr.WriteEndElement();
                    }

                    xtr.WriteStartElement("url");
                    xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("PetroBlog", "Home", new { blogPageName = routeTable.Get(EntityName.Blog, PageType.List) })}");
                    xtr.WriteEndElement();

                    var mlProducts = mL_ProductService.GetAllActiveLoaded().Where(x => x.WebSiteid == CurrentWebsiteId);


                    foreach (Language lang in LanguageContext.WebsiteLanguages)
                    {
                        ICollection<Category> Categories = new List<Category>();
                        try
                        {
                            Categories = categoryService.GetMany(x => x.WebSiteid == CurrentWebsite.id && x.IsActive == true, lang.id);
                            string pageTag = routeTable.Get(EntityName.Category, PageType.Detail, lang.KeyCode);

                            foreach (Category item in Categories)
                            {
                                xtr.WriteStartElement("url");
                                xtr.WriteAttributeString("xmlns", "xhtml", null, "http://www.w3.org/1999/xhtml");
                                try
                                {
                                    string link = Url.Action("CategoryDetail", "Detail", new { pageTag = pageTag, categoryName = GetFriendlyTitle(item.Name), id = item.id });
                                    xtr.WriteElementString("loc", $"{siteUrl}{link}");
                                    xtr.WriteElementString("lastmod", $"{(item.UpdateDate ?? item.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
                                }
                                catch
                                {
                                }
                                xtr.WriteEndElement();

                                xtr.WriteStartElement("url");

                                xtr.WriteAttributeString("xmlns", "xhtml", null, "http://www.w3.org/1999/xhtml");
                                try
                                {
                                    string link = Url.Action("CategoryDetailOld", "Detail", new { categoryName = GetFriendlyTitle(item.Name), page = 1, category = item.id });
                                    xtr.WriteElementString("loc", $"{siteUrl}{link}");
                                    xtr.WriteElementString("lastmod", $"{(item.UpdateDate ?? item.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
                                }
                                catch
                                {
                                }
                                xtr.WriteEndElement();

                            }
                        }
                        catch { }

                        try
                        {

                            ICollection<Product> Products = productService.GetMany(x => x.IsActive == true, lang.id);

                            var WebsiteProducts =
                                                from category in Categories
                                                join prod in Products on category.id equals prod.Categoryid
                                                select new { ProductName = prod.SupTitle, id = prod.id, UpdateDate = prod.UpdateDate, CreateDate = prod.CreateDate };

                            string pageTag = routeTable.Get(EntityName.Product, PageType.Detail, lang.KeyCode);

                            foreach (var item in WebsiteProducts)
                            {
                                xtr.WriteStartElement("url");
                                xtr.WriteAttributeString("xmlns", "xhtml", null, "http://www.w3.org/1999/xhtml");

                                try
                                {
                                    var alternatives = mlProducts.Where(x => x.ProductId == item.id && x.ProductLanguageKeyCode == lang.KeyCode).ToList();
                                    string link = Url.Action("ProductDetail", "Detail", new { produtname = GetFriendlyTitle(item.ProductName), id = item.id, pageTag = pageTag });
                                    xtr.WriteElementString("loc", $"{siteUrl}{link}");

                                    foreach (var alternate in alternatives)
                                    {
                                        string alternatepageTag = routeTable.Get(EntityName.Product, PageType.Detail, alternate.AlternateProductLanguageKeyCode);

                                        var alternateLink = Url.Action("ProductDetail", "Detail", new { produtname = GetFriendlyTitle(alternate.AlternateProduct.SupTitle), id = alternate.AlternateProduct.id, pageTag = alternatepageTag });

                                      xtr.WriteRaw($"<xhtml:link rel=\"alternate\" hreflang=\"{alternate.AlternateProductLanguageKeyCode}\"  href=\"{siteUrl}{alternateLink}\" />");
                                    }

                                    xtr.WriteElementString("lastmod", $"{(item.UpdateDate ?? item.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
                                }
                                catch
                                {
                                }
                                xtr.WriteEndElement();

                                xtr.WriteStartElement("url");
                                 xtr.WriteAttributeString("xmlns", "xhtml", null, "http://www.w3.org/1999/xhtml");
                                try
                                {
                                    string link = Url.Action("ProductDetailOld", "Detail", new { produtname = GetFriendlyTitle(item.ProductName), id = item.id });
                                    xtr.WriteElementString("loc", $"{siteUrl}{link}");
                                    xtr.WriteElementString("lastmod", $"{(item.UpdateDate ?? item.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
                                }
                                catch
                                {
                                }
                                xtr.WriteEndElement();


                            }
                        }
                        catch { }

                        try
                        {
                            ICollection<DynamicPage> dynamicPages = dynamicPageService.GetMany(x => x.WebSiteid == CurrentWebsite.id && x.IsActive == true, lang.id);

                            foreach (DynamicPage item in dynamicPages)
                            {
                                xtr.WriteStartElement("url");
                                try
                                {
                                    xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("DynamicPageView", "Home", new { pageName = GetFriendlyTitle(item.Name), id = item.id })}");
                                    xtr.WriteElementString("lastmod", $"{(item.UpdateDate ?? item.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
                                }
                                catch
                                {
                                }
                                xtr.WriteEndElement();
                            }
                        }
                        catch { }

                        try
                        {
                            ICollection<Blog> blogs = blogService.GetMany(x => x.WebSiteid == CurrentWebsite.id && x.IsActive == true, lang.id);

                            string blogPageName = routeTable.Get(EntityName.Blog, PageType.Detail, lang.KeyCode);

                            foreach (Blog item in blogs)
                            {
                                xtr.WriteStartElement("url");
                                try
                                {
                                    string link = Url.Action("BlogDetail", "Detail", new { title = GetFriendlyTitle(item.Title), id = item.id, blogPageName = blogPageName });
                                    xtr.WriteElementString("loc", $"{siteUrl}{link}");

                                    xtr.WriteElementString("lastmod", $"{(item.UpdateDate ?? item.CreateDate).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}+03:00");
                                }
                                catch
                                {
                                }
                                xtr.WriteEndElement();

                            }
                        }
                        catch { }
                    }

                    try
                    {
                        xtr.WriteEndElement();
                        xtr.WriteEndDocument();
                        xtr.Flush();
                        xtr.Close();
                    }
                    catch
                    {
                        return staticfileload();
                    }
                }
                catch
                {
                    return staticfileload();
                }


                string dir = Path.Combine(webHostEnvironment.WebRootPath, "Temp");
                Directory.CreateDirectory(dir);
                string sitemapfile = Path.Combine(dir, "sitemap.xml");
                System.IO.File.WriteAllText(sitemapfile, XDocument.Parse(sw.ToString()).ToString(), Encoding.UTF8);


                try
                {
                    using (StreamWriter streamWriter = new StreamWriter(Response.Body))
                    {
                        streamWriter.Write(XDocument.Parse(sw.ToString()).ToString());
                    }
                }
                catch
                {
                    return staticfileload();
                }
            }
            else
            {
                return staticfileload();
            }
            return View();
        }
    }
}