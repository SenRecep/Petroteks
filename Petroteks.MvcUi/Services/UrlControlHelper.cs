using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petroteks.MvcUi.ExtensionMethods;
using Microsoft.AspNetCore.Routing;
using System.Security.Policy;

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
