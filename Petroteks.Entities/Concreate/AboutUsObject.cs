using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.Concreate
{
    public class AboutUsObject : BasePage, IHtmlObject
    {
        public string Content { get; set; }
    }
}
