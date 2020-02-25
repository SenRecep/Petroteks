using Petroteks.Core.Dal;
using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;


namespace Petroteks.Bll.Abstract
{
    public interface IUserService : IEntityRepostory<User> { }
    public interface IWebsiteService : IEntityRepostory<Website> {
        public Website findByUrl(string url);
    }
    public interface IAboutUsObjectService : IEntityRepostory<AboutUsObject> { }
    public interface IPrivacyPolicyObjectService : IEntityRepostory<PrivacyPolicyObject> { }
    public interface ICategoryService : IEntityRepostory<Category> { }
    public interface IProductService : IEntityRepostory<Product> { }
    public interface IMainPageService : IEntityRepostory<MainPage> { }
    public interface IEmailService : IEntityRepostory<Email> { }
    public interface IBlogService : IEntityRepostory<Blog> { }
    public interface IDynamicPageService : IEntityRepostory<DynamicPage> { }
    public interface ILanguageService : IEntityRepostory<Language> { }
    public interface IUI_NavbarService : IEntityRepostory<UI_Navbar> { }
    public interface IUI_ContactService : IEntityRepostory<UI_Contact> { }
    public interface IUI_FooterService : IEntityRepostory<UI_Footer> { }
}
