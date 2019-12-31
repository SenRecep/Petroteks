using Petroteks.Core.Dal;
using Petroteks.Entities.Concreate;


namespace Petroteks.Bll.Abstract
{
    public interface IUserService : IEntityRepostory<User> { }
    public interface IWebsiteService : IEntityRepostory<Website> { }
    public interface ISliderObjectService : IEntityRepostory<SliderObject> { }
    public interface IAboutUsObjectService : IEntityRepostory<AboutUsObject> { }
    public interface IPrivacyPolicyObjectService : IEntityRepostory<PrivacyPolicyObject> { }
    public interface ICategoryService : IEntityRepostory<Category> { }
    public interface IProductService : IEntityRepostory<Product> { }
    public interface IMainPageService : IEntityRepostory<MainPage> { }
}
