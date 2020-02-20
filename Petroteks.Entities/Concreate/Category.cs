﻿using Petroteks.Entities.Abstract;
using Petroteks.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Concreate
{
    public class Category : ML_WebsiteObject, ICategory
    {
        public int Parentid { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
    }
}
