using Petroteks.Entities.Concreate;

namespace Petroteks.Entities.ComplexTypes
{
    public class ML_Product : WebsiteObject
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string ProductLanguageKeyCode { get; set; }

        public int AlternateProductId { get; set; }
        public Product AlternateProduct { get; set; }


        public string AlternateProductLanguageKeyCode { get; set; }
    }
}
