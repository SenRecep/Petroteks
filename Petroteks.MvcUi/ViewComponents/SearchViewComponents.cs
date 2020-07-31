using Microsoft.AspNetCore.Mvc;
using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.Models;

namespace Petroteks.MvcUi.ViewComponents
{
    public class AboutUsObjectVC : ViewComponent
    {
        public IViewComponentResult Invoke(SearchObject obj)
        {
            (AboutUsObject Ins, string Value, int Power) so = ((AboutUsObject)obj.Instance, obj.Value, obj.Power);
            return View(so);
        }
    }
    public class BlogVC : ViewComponent
    {
        public IViewComponentResult Invoke(SearchObject obj)
        {
            (Blog Ins, string Value, int Power) so = ((Blog)obj.Instance, obj.Value, obj.Power);
            return View(so);
        }
    }
    public class CategoryVC : ViewComponent
    {
        public IViewComponentResult Invoke(SearchObject obj)
        {
            (Category Ins, string Value, int Power) so = ((Category)obj.Instance, obj.Value, obj.Power);
            return View(so);
        }
    }
    public class DynamicPageVC : ViewComponent
    {
        public IViewComponentResult Invoke(SearchObject obj)
        {
            (DynamicPage Ins, string Value, int Power) so = ((DynamicPage)obj.Instance, obj.Value, obj.Power);
            return View(so);
        }
    }
    public class MainPageVC : ViewComponent
    {
        public IViewComponentResult Invoke(SearchObject obj)
        {
            (MainPage Ins, string Value, int Power) so = ((MainPage)obj.Instance, obj.Value, obj.Power);
            return View(so);
        }
    }
    public class PrivacyPolicyObjectVC : ViewComponent
    {
        public IViewComponentResult Invoke(SearchObject obj)
        {
            (PrivacyPolicyObject Ins, string Value, int Power) so = ((PrivacyPolicyObject)obj.Instance, obj.Value, obj.Power);
            return View(so);
        }
    }
    public class ProductVC : ViewComponent
    {
        public IViewComponentResult Invoke(SearchObject obj)
        {
            (Product Ins, string Value, int Power) so = ((Product)obj.Instance, obj.Value, obj.Power);
            return View(so);
        }
    }
    public class UI_ContactVC : ViewComponent
    {
        public IViewComponentResult Invoke(SearchObject obj)
        {
            (UI_Contact Ins, string Value, int Power) so = ((UI_Contact)obj.Instance, obj.Value, obj.Power);
            return View(so);
        }
    }
}
