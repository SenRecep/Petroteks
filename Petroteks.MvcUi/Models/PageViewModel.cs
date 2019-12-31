using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.Models
{
    public class PageViewModel
    {
        public string ContentHeader { get; set; }
        public string ContentHeaderBottom { get; set; }
        public string ContentBottom { get; set; }

        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
    }
}
