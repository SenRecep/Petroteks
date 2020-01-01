using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concreate;

namespace Petroteks.Bll.Concreate
{
    public class UserManager : EntityManager<User>, IUserService { public UserManager(IUserDal repostory) : base(repostory) { } }
    public class WebsiteManager : EntityManager<Website>, IWebsiteService { public WebsiteManager(IWebsiteDal repostory) : base(repostory) { }

        public Website findByUrl(string url)
        {
            Website wb = _repostory.Get(x=>x.BaseUrl.Equals(url));
            return wb;
        }
    }
    public class SliderObjectManager : EntityManager<SliderObject>, ISliderObjectService { public SliderObjectManager(ISliderObjectDal repostory) : base(repostory) { } }
    public class AboutUsObjectManager : EntityManager<AboutUsObject>, IAboutUsObjectService { public AboutUsObjectManager(IAboutUsObjectDal repostory) : base(repostory) { } }
    public class PrivacyPolicyObjectManager : EntityManager<PrivacyPolicyObject>, IPrivacyPolicyObjectService { public PrivacyPolicyObjectManager(IPrivacyPolicyObjectDal repostory) : base(repostory) { } }
    public class CategoryManager : EntityManager<Category>, ICategoryService { public CategoryManager(ICategoryDal repostory) : base(repostory) { } }
    public class ProductManager : EntityManager<Product>, IProductService { public ProductManager(IProductDal repostory) : base(repostory) { } }
    public class MainPageManager : EntityManager<MainPage>, IMainPageService { public MainPageManager(IMainPageDal repostory) : base(repostory) { } }
}
