using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Concreate;
using Petroteks.Bll.Helpers;
using Petroteks.Core.Dal;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Models;
using System;
using System.Collections.Generic;

namespace Petroteks.MvcUi.Controllers
{
    public class HomeController : GlobalController
    {
        private readonly IMainPageService mainPageService;

        public HomeController(IMainPageService mainPageService, IWebsiteService websiteService, IHttpContextAccessor httpContextAccessor) : base(websiteService, httpContextAccessor)
        {
            this.mainPageService = mainPageService;
        }

        public IActionResult Index()
        {
            MainPage mainPage; 
            mainPage = mainPageService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (mainPage==null)
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            return View(mainPage);
        }
    }
}
