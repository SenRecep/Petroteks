using Petroteks.Entities.Concreate;

namespace Petroteks.MvcUi.Services
{
    public interface IUserSessionService
    {
        User Get(string key);
        void Set(User value, string key);
    }
}
