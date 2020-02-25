using Petroteks.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.ComplexTypes
{
    public class UI_Footer : ML_WebsiteObject,IUI_Footer
    {
        public string Content { get; set; }
    }
}
