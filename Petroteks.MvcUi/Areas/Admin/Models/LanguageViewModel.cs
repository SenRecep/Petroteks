using Microsoft.AspNetCore.Http;

namespace Petroteks.MvcUi.Areas.Admin.Models
{
    public class LanguageViewModel
    {
        public string Name { get; set; }
        public string KeyCode { get; set; }
        public IFormFile IconCode { get; set; }
    }
}
