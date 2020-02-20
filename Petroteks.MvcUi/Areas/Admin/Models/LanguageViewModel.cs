using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.Areas.Admin.Models
{
    public class LanguageViewModel
    {
        public string Name { get; set; }
        public string KeyCode { get; set; }
        public IFormFile IconCode { get; set; }
    }
}
