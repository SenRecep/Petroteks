using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Petroteks.Bll.Abstract;

namespace Petroteks.MvcUi.Controllers
{
    public class PublicController : GlobalController
    {
        public PublicController(IWebsiteService websiteService, IHttpContextAccessor httpContextAccessor) : base(websiteService, httpContextAccessor)
        {
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.ThisWebsite = ThisWebsite;
            base.OnActionExecuted(context);
        }

    }
}