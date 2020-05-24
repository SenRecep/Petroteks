using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.Concreate
{
    public class Language : WebsiteObject, ILanguage
    {
        public string Name { get; set; }
        public string KeyCode { get; set; }
        public string IconCode { get; set; }
        public bool Default { get; set; }
    }
}
