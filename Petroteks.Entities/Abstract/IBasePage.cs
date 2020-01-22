using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Abstract
{
    public interface IBasePage
    {
        string Name { get; set; }
        string Keywords { get; set; }
        string Description { get; set; }
        string MetaTags { get; set; }
        string Title { get; set; }
    }
}
