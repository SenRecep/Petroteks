using Petroteks.Core.Entities;
using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.Concreate
{
    public class Product : EntityBase, IProduct, IHtmlObject
    {
        public int Categoryid { get; set; }
        public Category Category { get; set; }
        public string PhotoPath { get; set; }
        public string SupTitle { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }

        public string Keywords { get; set; }
        public string Description { get; set; }
        public string MetaTags { get; set; }
        public string Title { get; set; }

        public int Priority { get; set; }

        public int? Languageid { get; set; }
        public virtual Language Language { get; set; }
    }
}
