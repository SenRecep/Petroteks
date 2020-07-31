using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.Models
{
    public class SearchViewModel
    {
        public string SearchKey { get; set; }
        public List<SearchObject> Result { get; set; }
    }
}
