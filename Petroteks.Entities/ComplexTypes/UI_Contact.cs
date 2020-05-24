using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.ComplexTypes
{
    public class UI_Contact : ML_WebsiteObject, IUI_Contact
    {
        public string Content { get; set; }
    }
}
