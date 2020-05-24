using Microsoft.AspNetCore.Http;

namespace Petroteks.MvcUi.Models
{

    public class BlogViewModel
    {

        public string Title { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public IFormFile PhotoPath { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string MetaTags { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }
    }

}


