using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.Controllers
{
    public class ErrorController:Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
