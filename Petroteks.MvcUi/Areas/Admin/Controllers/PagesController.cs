using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
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
    }
}