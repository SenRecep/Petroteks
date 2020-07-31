using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Petroteks.MvcUi.ExtensionMethods;
using System;

namespace Petroteks.MvcUi.Services
{
    public class UrlControlHelper
    {
        private readonly IActionContextAccessor actionContextAccessor;
        public UrlControlHelper(IServiceProvider serviceProvider)
        {
            actionContextAccessor = serviceProvider.GetService<IActionContextAccessor>();
        }
        public (string area, string controller, string action) getCurrnetPage()
        {
            RouteData rd = actionContextAccessor.ActionContext.RouteData;
            string controller = rd.Values["controller"].ToString();
            string action = rd.Values["action"].ToString();
            string area = rd.Values["area"]?.ToString();
            return (area: area, controller: controller, action: action);
        }
    }
}
