using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Abstract
{
    public interface IBasePage
    {
        public string Keywords { get; set; }
        public string Description{ get; set; }
        public string MetaTags { get; set; }
        public string Title { get; set; }
    }
}
