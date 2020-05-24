using Petroteks.Entities.Abstract;
using Petroteks.Entities.ComplexTypes;

namespace Petroteks.Entities.Concreate
{
    public class Blog : ML_WebsiteObject, IBasePage, IHtmlObject
    {
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string MetaTags { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PhotoPath { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }
}
