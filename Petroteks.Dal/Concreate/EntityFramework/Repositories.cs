using Petroteks.Dal.Abstract;
using Petroteks.Dal.Concreate.EntityFramework.Contexts;
using Petroteks.Entities.ComplexTypes;
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
    public class EfDynamicPageDal : EfEntityRepostoryBase<DynamicPage, PetroteksDbContext>, IDynamicPageDal { }
    public class EfLanguageDal : EfEntityRepostoryBase<Language, PetroteksDbContext>, ILanguageDal { }
    public class EfUI_NavbarDal : EfEntityRepostoryBase<UI_Navbar, PetroteksDbContext>, IUI_NavbarDal { }
    public class EfUI_ContactDal : EfEntityRepostoryBase<UI_Contact, PetroteksDbContext>, IUI_ContactDal { }
    public class EfUI_FooterDal : EfEntityRepostoryBase<UI_Footer, PetroteksDbContext>, IUI_FooterDal { }
    public class EfUI_NoticeDal : EfEntityRepostoryBase<UI_Notice, PetroteksDbContext>, IUI_NoticeDal { }
}
