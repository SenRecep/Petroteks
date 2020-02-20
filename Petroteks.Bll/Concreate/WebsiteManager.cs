using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concreate;

namespace Petroteks.Bll.Concreate
{
    public class WebsiteManager : EntityManager<Website>, IWebsiteService
    {
        public WebsiteManager(IWebsiteDal repostory) : base(repostory) { }

        public Website findByUrl(string url)
        {
            Website wb = _repostory.Get(x => x.Name.Equals(url));
            return wb;
        }
    }
}
