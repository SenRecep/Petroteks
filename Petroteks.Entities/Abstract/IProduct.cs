using Petroteks.Entities.Concreate;

namespace Petroteks.Entities.Abstract
{
    public interface IProduct
    {
        int Categoryid { get; set; }
        Category Category { get; set; }
        string PhotoPath { get; set; }
        string SupTitle { get; set; }
        string SubTitle { get; set; }

        string Keywords { get; set; }
        string Description { get; set; }
        string MetaTags { get; set; }
        string Title { get; set; }

        int Priority { get; set; }

    }
}
