using Petroteks.Entities.Abstract;
using Petroteks.Entities.ComplexTypes;

namespace Petroteks.Entities.Concreate
{
    public class BasePage : ML_WebsiteObject, IBasePage
    {
        public string Name { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string MetaTags { get; set; }
        public string Title { get; set; }
    }
}
