using Petroteks.Entities.Concreate;
using System.Collections.Generic;

namespace Petroteks.MvcUi
{
    public class ProductListViewModel
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<Category> SubCategories { get; internal set; }

        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public Category CurrentCategory { get; set; }
        public int CurrentPage { get; set; }
    }
}