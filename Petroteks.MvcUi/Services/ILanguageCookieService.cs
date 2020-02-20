using Petroteks.Entities.Concreate;

namespace Petroteks.MvcUi.Services
{
    public interface ILanguageCookieService
    {
        void Set(string key, object value, int? expireTime);
        Language Get(string key);
        void Remove(string key);
    }
}
