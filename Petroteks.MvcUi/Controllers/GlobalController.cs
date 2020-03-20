using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;

namespace Petroteks.MvcUi.Controllers
{
    public class GlobalController : Controller
    {
        public Website ThisWebsite { get; set; }
        private readonly IWebsiteService websiteService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public GlobalController(IWebsiteService websiteService, IHttpContextAccessor httpContextAccessor)
        {
            this.websiteService = websiteService;
            this.httpContextAccessor = httpContextAccessor;
            if (ThisWebsite == null)
            {

                string url = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
                string siteName = httpContextAccessor.HttpContext.Request.Host.Value.Replace("www.","",System.StringComparison.InvariantCultureIgnoreCase);
                Website website = websiteService.findByUrl(siteName);
                if (website != null)
                    ThisWebsite = website;
                else
                {
                    Website wb = new Website()
                    {
                        BaseUrl = url,
                        Name = siteName
                    };
                    websiteService.Add(wb);
                    websiteService.Save();
                    ThisWebsite = wb;
                }

            }
        }
    }
}