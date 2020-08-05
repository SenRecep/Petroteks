using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.Areas.Admin.Models
{
    public class AlternateProductViewModel
    {
        public int ProductId { get; set; }
        public string KeyCode { get; set; }
        public int LanguageId { get; set; }
    }
}
