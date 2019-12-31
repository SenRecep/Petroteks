using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Concreate;
using Petroteks.Bll.Helpers;
using Petroteks.Core.Dal;
using Petroteks.Entities.Concrete;
using Petroteks.MvcUi.Models;
using System;
using System.Collections.Generic;

namespace Petroteks.MvcUi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
