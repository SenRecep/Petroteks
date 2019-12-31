using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Attributes;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Services;

namespace Petroteks.MvcUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        //_userSessionService.Get("LoginAdmin");
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public PagesController(IUserService userService, IUserSessionService userSessionService)
        {
            this._userService = userService;
            this._userSessionService = userSessionService;
        }

        public IActionResult HomePage()
        {
            return View();
        }
        public IActionResult AboutPage()
        {
            return View();
        }



        [AdminAuthorize]
        public IActionResult AnaSayfaEdit()
        {
            ViewBag.LoginUser = _userSessionService.Get("LoginAdmin");
            ViewBag.PageViewModel = new PageViewModel();
            return View();
        }


        [AdminAuthorize]
        [HttpPost]
        public IActionResult AnaSayfaEdit(MainPage model)
        {
            return View(model);
        }




    }
}