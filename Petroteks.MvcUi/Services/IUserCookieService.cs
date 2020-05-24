using Petroteks.Entities.Concreate;

namespace Petroteks.MvcUi.Services
{
    public interface IUserCookieService
    {
        void Set(string key, object value, int? expireTime);
        User Get(string key);
        void Remove(string key);
    }
}
