using Petroteks.Entities.Abstract;
using Petroteks.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.ComplexTypes
{
    public class UI_Contact : ML_WebsiteObject, IUI_Contact
    {
        public string Content { get; set; }
    }
}
