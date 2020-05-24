using System;

namespace Petroteks.Entities.Abstract
{
    public interface IUI_Notice
    {
        string Content { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
