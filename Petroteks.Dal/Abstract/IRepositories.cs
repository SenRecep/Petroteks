using Petroteks.Core.Dal;
using Petroteks.Entities.ComplexTypes;
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
    public interface IDynamicPageDal : IEntityRepostory<DynamicPage> { }
    public interface ILanguageDal : IEntityRepostory<Language> { }
    public interface IUI_NavbarDal : IEntityRepostory<UI_Navbar> { }
    public interface IUI_ContactDal : IEntityRepostory<UI_Contact> { }
    public interface IUI_FooterDal : IEntityRepostory<UI_Footer> { }
    public interface IUI_NoticeDal : IEntityRepostory<UI_Notice> { }

}
