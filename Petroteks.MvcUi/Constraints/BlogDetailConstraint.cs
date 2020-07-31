using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Petroteks.Bll.Concreate;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.Entities.Concreate;

namespace Petroteks.MvcUi.Constraints
{
    public class BlogDetailConstraint : IRouteConstraint
    {
        private readonly IRouteTable routeTable;

        public BlogDetailConstraint(IRouteTable routeTable)
        {
            this.routeTable = routeTable;
        }
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return routeTable.Exists(values[routeKey].ToString(), EntityName.Blog, PageType.Detail);
        }
    }
}
