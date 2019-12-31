using Petroteks.Entities.Concreate;

namespace Petroteks.Entities.Abstract
{
    public interface IWebsiteObject
    {
        int WebSiteid { get; set; }
        Website WebSite { get; set; }
    }
}
