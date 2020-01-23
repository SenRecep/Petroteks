using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.Models
{
    public class MainPageViewModel
    {
        public MainPage MainPage { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
