using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concreate;

namespace Petroteks.Bll.Concreate
{
    public class UserManager : EntityManager<User>, IUserService { public UserManager(IUserDal repostory) : base(repostory) { } }
}
