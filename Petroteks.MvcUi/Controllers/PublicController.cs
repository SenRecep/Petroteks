using Microsoft.AspNetCore.Http;
using Petroteks.Bll.Abstract;
using Petroteks.MvcUi.Services;

namespace Petroteks.MvcUi.Controllers
{
    public class PublicController : GlobalController
    {
        public PublicController(IWebsiteService websiteService, ILanguageService languageService, ILanguageCookieService languageCookieService, IHttpContextAccessor httpContextAccessor) : base(websiteService, languageService, languageCookieService, httpContextAccessor)
        {
        }
        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    ViewBag.CurrentWebsite = Bll.Helpers.WebsiteContext.CurrentWebsite;
        //    base.OnActionExecuted(context);
        //}

    }
}