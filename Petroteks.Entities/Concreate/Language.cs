using Petroteks.Core.Entities;
using Petroteks.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Concreate
{
    public class Language:WebsiteObject,ILanguage
    {
        public string Name { get; set; }
        public string KeyCode { get; set; }
        public string IconCode { get; set; }
        public bool Default { get; set; }
    }
}
