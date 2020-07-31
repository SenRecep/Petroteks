using Microsoft.AspNetCore.Http;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.ExtensionMethods;
using System.Collections.Generic;

namespace Petroteks.MvcUi.Services
{
    public class WebsiteCookieService : IWebsiteCookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WebsiteCookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Website Get(string key)
        {
            return _httpContextAccessor.HttpContext.Session.GetObj<Website>(key);
        }


        public void Set(string key, object value, int? expireTime)
        {
            _httpContextAccessor.HttpContext.Session.SetObj(key, value);
        }

        public void Remove(string key)
        {
            _httpContextAccessor.HttpContext.Session.Remove(key);
        }
    }
}
