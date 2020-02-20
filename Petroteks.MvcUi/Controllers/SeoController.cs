using System.Text;
using System.Xml;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using static Petroteks.Bll.Helpers.FriendlyUrlHelper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petroteks.MvcUi.Services;

namespace Petroteks.MvcUi.Controllers
{
    public class SeoController : GlobalController
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IDynamicPageService dynamicPageService;

        public SeoController(ICategoryService categoryService,
            IWebsiteService websiteService,
            IProductService productService,
            ILanguageCookieService languageCookieService,
            ILanguageService languageService,
            IDynamicPageService dynamicPageService,
            IHttpContextAccessor httpContextAccessor)
            : base(websiteService,languageService,languageCookieService, httpContextAccessor)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.dynamicPageService = dynamicPageService;
        }


        [Route("sitemap.xml")]
        public IActionResult SitemapXml()
        {
            var siteUrl = $"{Request.Scheme}://{Request.Host}";
            Response.Clear();
            Response.ContentType = "text/xml";
            XmlTextWriter xtr = new XmlTextWriter(Response.Body, Encoding.UTF8);
            xtr.WriteStartDocument();
            xtr.WriteStartElement("urlset");
            xtr.WriteAttributeString("xmlns", "http://www.sitemap.org/schemas/sitemap/0.9");
            xtr.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xtr.WriteAttributeString("xsi:schemaLocation", "http://www.sitemap.org/schemas/sitemap/0.9 http://www.sitemap.org/schemas/sitemap/0.9/siteindex.xsd");

            xtr.WriteStartElement("url");
            xtr.WriteElementString("loc", $"{siteUrl}/");
            xtr.WriteEndElement();


            xtr.WriteStartElement("url");
            xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("AboutUs", "Home")}");
            xtr.WriteEndElement();

            xtr.WriteStartElement("url");
            xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("PrivacyPolicy", "Home")}");
            xtr.WriteEndElement();

            xtr.WriteStartElement("url");
            xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("Contact", "Home")}");
            xtr.WriteEndElement();

            xtr.WriteStartElement("url");
            xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("PetroBlog", "Home")}");
            xtr.WriteEndElement();

            ICollection<Category> Categories = new List<Category>();

            try
            {
                Categories = categoryService.GetMany(x => x.WebSiteid == ThisWebsite.id && x.IsActive == true);

                foreach (Category item in Categories)
                {
                    xtr.WriteStartElement("url");
                    xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("CategoryDetail", "Detail", new { categoryName = GetFriendlyTitle(item.Name), page = 1, category = item.id })}");
                    xtr.WriteEndElement();
                }
            }
            catch { }

            try
            {
                ICollection<Product> Products = productService.GetMany(x => x.IsActive == true);

                var WebsiteProducts =
                                    from category in Categories
                                    join prod in Products on category.id equals prod.Categoryid
                                    select new { ProductName = prod.SupTitle, id = prod.id };


                foreach (var item in WebsiteProducts)
                {
                    xtr.WriteStartElement("url");
                    xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("ProductDetail", "Detail", new { produtname = GetFriendlyTitle(item.ProductName), id = item.id })}");
                    xtr.WriteEndElement();
                }
            }
            catch { }

            try
            {
                ICollection<DynamicPage> dynamicPages = dynamicPageService.GetMany(x => x.WebSiteid == ThisWebsite.id && x.IsActive == true);

                foreach (var item in dynamicPages)
                {
                    xtr.WriteStartElement("url");
                    xtr.WriteElementString("loc", $"{siteUrl}{Url.Action("DynamicPageView", "Home", new { pageName = GetFriendlyTitle(item.Name), id = item.id })}");
                    xtr.WriteEndElement();
                }
            }
            catch { }
            xtr.WriteEndElement();
            xtr.WriteEndDocument();
            xtr.Flush();
            xtr.Close();

            return View();
        }
    }
}