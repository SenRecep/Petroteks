using Petroteks.Bll.Abstract;
using Petroteks.MvcUi.Models.MI;
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


        public ICollection<MI_Category> MainCategories { get; set; }
        public ICollection<MI_Category> AllSubCategory { get; set; }
        public ICollection<MI_Product> AllProduct { get; set; }

        public ICollection<MI_Category> GetCategoryies(int parentId)
        {
            return AllSubCategory.Where(x => x.Parentid == parentId).ToList();
        }
        public ICollection<MI_Product> GetProducts(int categoryId)
        {
            return AllProduct.Where(x => x.Categoryid == categoryId).ToList();
        }

        public ICollection<MenuItem> GetItems(int categoryId)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            menuItems.AddRange(GetCategoryies(categoryId));
            menuItems.AddRange(GetProducts(categoryId));
            menuItems = menuItems.OrderByDescending(x => x.Priority).ToList();
            return menuItems;
        }

        public string NameId(MI_Category category)
        {
            return $"[{category.id}] {category.Name}";
        }

    }
}
