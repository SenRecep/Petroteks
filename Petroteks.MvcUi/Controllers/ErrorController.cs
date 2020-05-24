using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Petroteks.MvcUi.Controllers
{
    public class ErrorController : Controller
    {

        [Route("Error/500")]
        public IActionResult Error500()
        {
            IExceptionHandlerPathFeature exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                ViewBag.ErrorMessage = exceptionFeature.Error.Message;
                ViewBag.RouteOfException = exceptionFeature.Path;
            }
            return View();
        }

        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            IStatusCodeReExecuteFeature statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry the page you requested could not be found";
                    ViewBag.RouteOfException = statusCodeData.OriginalPath;
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Sorry something went wrong on the server";
                    ViewBag.RouteOfException = statusCodeData.OriginalPath;
                    break;
            }
            ViewBag.StatusCode = statusCode;
            return View();
        }

        [Route("Error/404")]
        public IActionResult HandlePageNotFound()
        {
            return View();
        }
    }
}