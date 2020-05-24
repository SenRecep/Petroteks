using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Controllers;
using Petroteks.MvcUi.Services;

namespace Petroteks.MvcUi.Areas.Admin.Controllers
{
    public class AdminBaseController : GlobalController
    {
        private readonly IUserSessionService userSessionService;

        public AdminBaseController(IUserSessionService userSessionService, IWebsiteService websiteService, ILanguageService languageService, ILanguageCookieService languageCookieService, IHttpContextAccessor httpContextAccessor)
            : base(websiteService, languageService, languageCookieService, httpContextAccessor)
        {
            this.userSessionService = userSessionService;
        }
        public User LoginUser
        {
            get
            {
                ViewBag.LoginUser = userSessionService.Get("LoginAdmin");
                return userSessionService.Get("LoginAdmin");
            }
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.LoginUser = userSessionService.Get("LoginAdmin");
            base.OnActionExecuted(context);
        }
    }
}