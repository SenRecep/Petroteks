using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Abstract
{
    public interface ILanguage
    {
        string Name { get; set; }
        string KeyCode { get; set; }
        string IconCode { get; set; }
        bool Default { get; set; }
    }
}
