using Microsoft.AspNetCore.Mvc;

namespace Petroteks.MvcUi.ViewComponents
{
    public class SearchInputForm:ViewComponent
    {
        public IViewComponentResult Invoke(string s){
            ViewBag.SearchKey=s;
            return View();
        } 
    }
}