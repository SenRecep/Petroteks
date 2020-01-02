using Microsoft.AspNetCore.Mvc;
using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public CategoryList(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }


        public async Task<IViewComponentResult> InvokeAsync(Website website)
        {
            return View(new CategoryListViewModel(categoryService)
            {
                MainCategories = categoryService.GetMany(category => category.WebSiteid == website.id && category.Parentid == 0),
                AllSubCategory = categoryService.GetMany(category => category.WebSiteid == website.id && category.Parentid != 0)
            });
        }
    }

    public class CategoryListViewModel
    {
        private readonly ICategoryService categoryService;

        public CategoryListViewModel(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public CategoryViewModel CategoryViewModel { get; set; }

        public ICollection<Category> MainCategories { get; set; }
        public ICollection<Category> AllSubCategory { get; set; }

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
