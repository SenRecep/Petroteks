using Petroteks.Dal.Abstract;
using Petroteks.Dal.Concreate.EntityFramework.Contexts;
using Petroteks.Entities.Concrete;

namespace Petroteks.Dal.Concreate.EntityFramework
{
    public class EfUserDal : EfEntityRepostoryBase<User, PetroteksDbContext>, IUserDal { }
}
