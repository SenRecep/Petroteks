using Petroteks.Entities.Concreate;
using System.Collections.Generic;

namespace Petroteks.Bll.Helpers
{
    public static class WebsiteContext
    {
        public static Website CurrentWebsite { get; set; }
        public static ICollection<Website> Websites { get; set; }
    }
}
