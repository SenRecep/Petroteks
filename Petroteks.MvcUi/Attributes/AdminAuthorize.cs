using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Petroteks.MvcUi.Services;
using System;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.Entities.Concreate;

namespace Petroteks.MvcUi.Attributes
{
    public class AdminAuthorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            User loginUser = filterContext.HttpContext.Session.GetObj<User>("LoginAdmin");
            if (loginUser != null)
            {
                if (loginUser.Role == 2)
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Admin", controller = "Home", action = "Login" }));
            }
            else
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Admin", controller = "Home", action = "Login" }));
        }
    }
}
