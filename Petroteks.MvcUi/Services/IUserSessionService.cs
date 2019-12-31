using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.Services
{
    public interface IUserSessionService
    {
        User Get(string key);
        void Set(User value, string key);
    }
}
