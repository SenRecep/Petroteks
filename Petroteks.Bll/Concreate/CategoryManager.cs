using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Petroteks.Bll.Helpers.LanguageContext;
namespace Petroteks.Bll.Concreate
{
    public class CategoryManager : EntityManager<Category>, ICategoryService
    {
        public CategoryManager(ICategoryDal repostory) : base(repostory)
        {
        }
        public override Category Get(Expression<Func<Category, bool>> filter, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.Get(filter, navigations);
        }

        public Category GetAllLanguageCategory(Expression<Func<Category, bool>> filter, params string[] navigations)
        {
            return base.Get(filter, navigations);
        }

        public override ICollection<Category> GetMany(Expression<Func<Category, bool>> filter = null, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.GetMany(filter, navigations);
        }
    }
}
