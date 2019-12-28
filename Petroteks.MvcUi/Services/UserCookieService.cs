using Microsoft.AspNetCore.Http;
using Petroteks.Entities.Concrete;
using System.Collections.Generic;
using Petroteks.MvcUi.ExtensionMethods;

namespace Petroteks.MvcUi.Services
{
    public class UserCookieService : IUserCookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserCookieService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public User Get(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies.Get<User>(key);
        }


        public void Set(string key, object value, int? expireTime)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Set(key, value, expireTime);
        }

        public void Remove(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Remove(key);
        }
    }
}
