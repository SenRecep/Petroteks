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

        public GlobalController( IWebsiteService websiteService, IHttpContextAccessor httpContextAccessor)
        {
            this.websiteService = websiteService;
            this.httpContextAccessor = httpContextAccessor;
            if (ThisWebsite == null)
            {

                string url = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
                Website website = websiteService.findByUrl(url);
                if (website != null)
                    ThisWebsite = website;
                else
                {
                    Website wb = new Website()
                    {
                        BaseUrl = url,
                        Name = httpContextAccessor.HttpContext.Request.Host.Value
                    };
                    websiteService.Add(wb);
                    websiteService.Save();
                    ThisWebsite = wb;
                }

            }
        }
    }
}