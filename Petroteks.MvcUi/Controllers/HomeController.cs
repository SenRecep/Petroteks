using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Petroteks.MvcUi.Models;

namespace Petroteks.MvcUi.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
