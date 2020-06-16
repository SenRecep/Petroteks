using Microsoft.AspNetCore.Http;
using Petroteks.Bll.Abstract;
using Petroteks.MvcUi.Services;

namespace Petroteks.MvcUi.Controllers
{
    public class PublicController : GlobalController
    {
        public PublicController(IWebsiteService websiteService, ILanguageService languageService, ILanguageCookieService languageCookieService, IHttpContextAccessor httpContextAccessor) : base(websiteService, languageService, languageCookieService, httpContextAccessor)
        {
        }
        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    var assemblies = new List<Assembly>();
        //    AppDomain.CurrentDomain.GetAssemblies().ToList().ForEach(a =>
        //    {
        //        if (a.FullName.Contains("Petroteks"))
        //            assemblies.Add(a);
        //    });
        //    var types = new List<Type>();
        //    assemblies.ForEach(x => types.AddRange(x.GetTypes()));
        //    base.OnActionExecuted(context);
        //}
    }
}