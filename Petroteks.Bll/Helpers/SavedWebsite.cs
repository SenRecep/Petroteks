using Petroteks.Entities.Concreate;
using System.Collections.Generic;

namespace Petroteks.Bll.Helpers
{
    public class SavedWebsite
    {
        public Website PublicWebsite { get; set; }
        public ICollection<SavedAdminWebsite> SavedAdminWebsites { get; set; }
    }
    public class SavedAdminWebsite
    {
        public int AdminId { get; set; }
        public int WebsiteId { get; set; }
    }
}
