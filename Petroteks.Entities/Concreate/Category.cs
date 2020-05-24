using Petroteks.Entities.Abstract;
using Petroteks.Entities.ComplexTypes;

namespace Petroteks.Entities.Concreate
{
    public class Category : ML_WebsiteObject, ICategory
    {
        public int Parentid { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public int Priority { get; set; }
    }
}
