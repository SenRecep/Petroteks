﻿using Petroteks.Entities.Abstract;
using Petroteks.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Concreate
{
    public class BasePage :ML_WebsiteObject, IBasePage
    {
        public string Name { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string MetaTags { get; set; }
        public string Title { get; set; }
    }
}
