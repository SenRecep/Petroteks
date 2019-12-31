using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.Concreate
{
    public class HtmlObject :WebsiteObject, IHtmlObject
    {
        public string Content { get; set; }
    }
}
