using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.ComplexTypes
{
    public class UI_Navbar : ML_WebsiteObject, IUI_Navbar
    {
        public string Home { get; set; }
        public string Products { get; set; }
        public string AboutUs { get; set; }
        public string PetroBlog { get; set; }
        public string Contact { get; set; }
        public string Pages { get; set; }
        public string Languages { get; set; }
    }
}
