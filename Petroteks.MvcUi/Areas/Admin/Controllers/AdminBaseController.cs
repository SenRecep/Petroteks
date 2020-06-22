using Microsoft.AspNetCore.Mvc.Filters;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Controllers;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Services;
using System;

namespace Petroteks.MvcUi.Areas.Admin.Controllers
{
    public class AdminBaseController : GlobalController
    {
        private readonly IUserSessionService userSessionService;

        public AdminBaseController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            userSessionService = serviceProvider.GetService<IUserSessionService>();
        }
        public User LoginUser => userSessionService.Get("LoginAdmin");
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.LoginUser = LoginUser;
            base.OnActionExecuting(context);
        }
    }
}