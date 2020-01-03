using Petroteks.Core.Dal;
using Petroteks.Entities.Concreate;

namespace Petroteks.Dal.Abstract
{
    public interface IUserDal : IEntityRepostory<User> { }
    public interface IWebsiteDal : IEntityRepostory<Website> { }
    public interface IAboutUsObjectDal : IEntityRepostory<AboutUsObject> { }
    public interface IPrivacyPolicyObjectDal : IEntityRepostory<PrivacyPolicyObject> { }
    public interface ICategoryDal : IEntityRepostory<Category> { }
    public interface IProductDal : IEntityRepostory<Product> { }
    public interface IMainPageDal : IEntityRepostory<MainPage> { }
    public interface IEmailDal : IEntityRepostory<Email> { }
    public interface IBlogDal : IEntityRepostory<Blog> { }
}
