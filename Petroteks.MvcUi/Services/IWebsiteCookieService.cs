using Petroteks.Entities.Concreate;

namespace Petroteks.MvcUi.Services
{
    public interface IWebsiteCookieService
    {
        void Set(string key, object value, int? expireTime);
        Website Get(string key);
        void Remove(string key);
    }
}
