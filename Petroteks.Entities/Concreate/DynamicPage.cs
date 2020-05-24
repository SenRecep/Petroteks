using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.Concreate
{
    public class DynamicPage : BasePage, IDynamicPage
    {
        public string Content { get; set; }
    }
}
