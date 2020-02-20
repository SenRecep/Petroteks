using Petroteks.Entities.Abstract;
using Petroteks.Entities.ComplexTypes;

namespace Petroteks.Entities.Concreate
{
    public class HtmlObject : ML_WebsiteObject, IHtmlObject
    {
        public string Content { get; set; }
    }
}
