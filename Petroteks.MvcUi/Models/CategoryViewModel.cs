using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.Models
{
    public class CategoryViewModel
    {
        public int id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        public int Priority { get; set; }
    }
}
