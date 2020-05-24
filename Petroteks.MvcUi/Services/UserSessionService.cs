using Microsoft.AspNetCore.Http;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.ExtensionMethods;

namespace Petroteks.MvcUi.Services
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserSessionService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }


        public User Get(string key)
        {
            return httpContextAccessor.HttpContext.Session.GetObj<User>(key);
        }

        public void Set(User value, string key)
        {
            httpContextAccessor.HttpContext.Session.SetObj(key, value);
        }
    }
}
