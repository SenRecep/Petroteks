using Petroteks.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Concreate
{
    public class Product : WebsiteObject,IProduct, IHtmlObject
    {
        public int Categoryid { get; set; }
        public ICategory Category { get; set; }
        public string PhotoPath { get; set; }
        public string SupTitle { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }
    }
}
