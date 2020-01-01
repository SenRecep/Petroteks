using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Attributes;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Services;

namespace Petroteks.MvcUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : AdminBaseController
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;
        private readonly IMainPageService mainPageService;

        public PagesController(IUserService userService, IUserSessionService userSessionService, IMainPageService mainPageService,IWebsiteService websiteService, IHttpContextAccessor  httpContextAccessor) :base(userSessionService, websiteService,httpContextAccessor)
        {
            this._userService = userService;
            this._userSessionService = userSessionService;
            this.mainPageService = mainPageService;
        }

       

        [AdminAuthorize]
        public IActionResult AnaSayfaEdit()
        {
            MainPage mainPage;
            mainPage = mainPageService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (mainPage==null)
                mainPage = new MainPage();
            return View(mainPage);
        }


        [AdminAuthorize]
        [HttpPost]
        public IActionResult AnaSayfaEdit(MainPage model)
        {
            MainPage mainPage;
            mainPage = mainPageService.Get(x => x.WebSiteid == ThisWebsite.id);
            if (mainPage==null)
            {
                mainPage = model;
                mainPage.WebSite = ThisWebsite;
                mainPageService.Add(mainPage);
            }
            else
            {
                mainPage.Keywords = model.Keywords;
                mainPage.Title = model.Title;
                mainPage.MetaTags = model.MetaTags;
                mainPage.TopContent = model.TopContent;
                mainPage.BottomContent = model.BottomContent;
                mainPage.Description = model.Description;
                mainPage.Slider = model.Slider;
                mainPageService.Update(mainPage);
            }
            mainPageService.Save();
            return View(mainPage);
        }
    }
}