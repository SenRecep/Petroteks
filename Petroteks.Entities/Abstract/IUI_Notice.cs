using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Abstract
{
    public interface IUI_Notice
    {
        string Content { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
