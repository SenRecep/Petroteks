using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.Concreate
{
    public class WebsiteObject : IWebsiteObject
    {
        public int WebSiteid { get; set; }
        public IWebsite WebSite { get; set; }
    }
}
