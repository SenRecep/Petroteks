using System.Collections.Generic;
using Petroteks.Entities.Concreate;

namespace Petroteks.MvcUi.Areas.Admin.Models
{
    public class WCPViewModel
    {
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
    }

    public class WLCViewModel
    {
        public Language Language { get; set; }
        public List<WCPViewModel> Categories { get; set; }
    }

}
