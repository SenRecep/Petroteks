using Petroteks.Core.Dal;
using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;
using System;
using System.Linq.Expressions;

namespace Petroteks.Bll.Abstract
{
    public interface IUserService : IEntityRepostory<User> { }
    public interface IWebsiteService : IEntityRepostory<Website>
    {
        public Website findByUrl(string url);
    }
    public interface IAboutUsObjectService : IEntityRepostory<AboutUsObject>, LanguageEntityManager<AboutUsObject> { }
    public interface IPrivacyPolicyObjectService : IEntityRepostory<PrivacyPolicyObject>, LanguageEntityManager<PrivacyPolicyObject> { }
    public interface ICategoryService : IEntityRepostory<Category>, LanguageEntityManager<Category>
    {
        public Category GetAllLanguageCategory(Expression<Func<Category, bool>> filter, params string[] navigations);
    }
    public interface IProductService : IEntityRepostory<Product>, LanguageEntityManager<Product>
    {
        public Product GetAllLanguageProduct(Expression<Func<Product, bool>> filter, params string[] navigations);
    }
    public interface IMainPageService : IEntityRepostory<MainPage>, LanguageEntityManager<MainPage> { }
    public interface IEmailService : IEntityRepostory<Email> { }
    public interface IBlogService : IEntityRepostory<Blog>, LanguageEntityManager<Blog>
    {
        public Blog GetAllLanguageBlog(Expression<Func<Blog, bool>> filter, params string[] navigations);
    }
    public interface IDynamicPageService : IEntityRepostory<DynamicPage>, LanguageEntityManager<DynamicPage> { }
    public interface ILanguageService : IEntityRepostory<Language> { }
    public interface IUI_NavbarService : IEntityRepostory<UI_Navbar>, LanguageEntityManager<UI_Navbar> { }
    public interface IUI_ContactService : IEntityRepostory<UI_Contact>, LanguageEntityManager<UI_Contact> { }
    public interface IUI_FooterService : IEntityRepostory<UI_Footer>, LanguageEntityManager<UI_Footer> { }
    public interface IUI_NoticeService : IEntityRepostory<UI_Notice>, LanguageEntityManager<UI_Notice> { }
}
