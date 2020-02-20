using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.ComplexTypes
{
    public class ML_WebsiteObject: WebsiteObject
    {
        public int? Languageid { get; set; }
        public  virtual Language Language { get; set; }
    }
}
