using System.Linq;
using System.Collections.Generic;
using System.Text;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concrete;
using Petroteks.Bll.Abstract;

namespace Petroteks.Bll.Concreate
{
    public class UserManager : EntityManager<User>, IUserService
    {
        public UserManager(IUserDal repostory) : base(repostory) { }
    }
}