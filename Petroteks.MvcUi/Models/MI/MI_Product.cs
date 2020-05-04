using Petroteks.Entities.Concreate;

namespace Petroteks.MvcUi.Models.MI
{
    public class MI_Product : MenuItem
    {
        public MI_Product(Product product)
        {
            base.IsProduct = true;
            base.Priority = product.Priority;
            id = product.id;
            SubTitle = product.SubTitle;
            SupTitle = product.SupTitle;
            Categoryid = product.Categoryid;
        }
        public int Categoryid { get; set; }
        public string SupTitle { get; set; }
        public string SubTitle { get; set; }
    }
}
