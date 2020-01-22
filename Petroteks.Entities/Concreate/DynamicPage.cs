using Petroteks.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Concreate
{
    public class DynamicPage : BasePage, IDynamicPage
    {
        public string Content { get;set; }
    }
}
