using Petroteks.Dal.Abstract;
using Petroteks.Dal.Concreate.EntityFramework.Contexts;
using Petroteks.Entities.Concreate;

namespace Petroteks.Dal.Concreate.EntityFramework
{
    public class EfUserDal : EfEntityRepostoryBase<User, PetroteksDbContext>, IUserDal { }
    public class EfWebsiteDal : EfEntityRepostoryBase<Website, PetroteksDbContext>, IWebsiteDal { }
    public class EfAboutUsObjectDal : EfEntityRepostoryBase<AboutUsObject, PetroteksDbContext>, IAboutUsObjectDal { }
    public class EfPrivacyPolicyObjectDal : EfEntityRepostoryBase<PrivacyPolicyObject, PetroteksDbContext>, IPrivacyPolicyObjectDal { }
    public class EfCategoryDal : EfEntityRepostoryBase<Category, PetroteksDbContext>, ICategoryDal { }
    public class EfProductDal : EfEntityRepostoryBase<Product, PetroteksDbContext>, IProductDal { }
    public class EfMainPageDal : EfEntityRepostoryBase<MainPage, PetroteksDbContext>, IMainPageDal { }
    public class EfEmailDal : EfEntityRepostoryBase<Email, PetroteksDbContext>, IEmailDal { }
    public class EfBlogDal : EfEntityRepostoryBase<Blog, PetroteksDbContext>, IBlogDal { }
}
