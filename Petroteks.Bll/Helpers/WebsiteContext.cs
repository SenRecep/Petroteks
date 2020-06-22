using Petroteks.Entities.Concreate;
using System.Collections.Generic;

namespace Petroteks.Bll.Helpers
{
    public static class WebsiteContext
    {
        //public static SavedWebsite SavedWebsite { get; set; }
        public static ICollection<Website> Websites { get; set; }
    }
}
