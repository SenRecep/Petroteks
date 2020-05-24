using Petroteks.Entities.Concreate;

namespace Petroteks.Entities.ComplexTypes
{
    public class ML_WebsiteObject : WebsiteObject
    {
        public int? Languageid { get; set; }
        public virtual Language Language { get; set; }
    }
}
