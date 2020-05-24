using Petroteks.Core.Entities;
using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.Concreate
{
    public class WebsiteObject : EntityBase, IWebsiteObject
    {
        public int WebSiteid { get; set; }
        public Website WebSite { get; set; }
    }
}
