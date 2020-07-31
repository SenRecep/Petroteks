using System;
using System.Collections.Generic;
using Petroteks.Bll.Abstract;
using Petroteks.Core.Dal;
using Petroteks.Core.Entities;
using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;

namespace Petroteks.MvcUi.ExtensionMethods
{
    public static class ServiceProviderExtensionMethod
    {
        public static T GetService<T>(this IServiceProvider provider)
        {
            return (T)provider.GetService(typeof(T));
        }

        public static IEntityRepostory<T> GetDbService<T>(this IServiceProvider provider) where T : EntityBase, new()
        {
            Type type = typeof(T);

            if (type==null)
                return null;

            if (type == typeof(User))
                return (IEntityRepostory<T>)provider.GetService<IUserService>();

            if (type == typeof(Website))
                return (IEntityRepostory<T>)provider.GetService<IWebsiteService>();

            if (type == typeof(AboutUsObject))
                return (IEntityRepostory<T>)provider.GetService<IAboutUsObjectService>();

            if (type == typeof(PrivacyPolicyObject))
                return (IEntityRepostory<T>)provider.GetService<IPrivacyPolicyObjectService>();

            if (type == typeof(MainPage))
                return (IEntityRepostory<T>)provider.GetService<IMainPageService>();

            if (type == typeof(Category))
                return (IEntityRepostory<T>)provider.GetService<ICategoryService>();

            if (type == typeof(Product))
                return (IEntityRepostory<T>)provider.GetService<IProductService>();

            if (type == typeof(Email))
                return (IEntityRepostory<T>)provider.GetService<IEmailService>();

            if (type == typeof(Blog))
                return (IEntityRepostory<T>)provider.GetService<IBlogService>();

            if (type == typeof(DynamicPage))
                return (IEntityRepostory<T>)provider.GetService<IDynamicPageService>();

            if (type == typeof(Language))
                return (IEntityRepostory<T>)provider.GetService<ILanguageService>();

            if (type == typeof(UI_Navbar))
                return (IEntityRepostory<T>)provider.GetService<IUI_NavbarService>();

            if (type == typeof(UI_Contact))
                return (IEntityRepostory<T>)provider.GetService<IUI_ContactService>();

            if (type == typeof(UI_Footer))
                return (IEntityRepostory<T>)provider.GetService<IUI_FooterService>();

            if (type == typeof(UI_Notice))
                return (IEntityRepostory<T>)provider.GetService<IUI_NoticeService>();

            return null;
        }
    }
}
