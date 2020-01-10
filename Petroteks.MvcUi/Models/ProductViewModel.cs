using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.Models
{
    public class ProductViewModel
    {
        public int Categoryid { get; set; }
        public IFormFile Image { get; set; }
        public string SupTitle { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }

        public string Keywords { get; set; }
        public string Description { get; set; }
        public string MetaTags { get; set; }
        public string Title { get; set; }

        public bool IsActive { get; set; }
    }
}
