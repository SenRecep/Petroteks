using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using System.Collections.Generic;
using System.Linq;

namespace Petroteks.MvcUi.Models
{
    public class CategoryListViewModel
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public CategoryListViewModel(ICategoryService categoryService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        public CategoryViewModel CategoryViewModel { get; set; }

        public ICollection<Category> MainCategories { get; set; }
        public ICollection<Category> AllSubCategory { get; set; }
        public ICollection<Product> AllProduct { get; set; }

        public ICollection<Category> GetCategoryies(int parentId)
        {
            return AllSubCategory.Where(x => x.Parentid == parentId).ToList();
        }

        public string NameId(Category category)
        {
            return $"[{category.id}] {category.Name}";
        }

    }
}
