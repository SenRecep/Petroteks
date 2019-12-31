using Petroteks.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Concreate
{
    public class Category : WebsiteObject, ICategory
    {
        public int Parentid { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
    }
}
