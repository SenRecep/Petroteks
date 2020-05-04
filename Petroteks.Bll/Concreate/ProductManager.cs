using Petroteks.Bll.Abstract;
using Petroteks.Bll.Helpers;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Petroteks.Bll.Concreate
{
    public class ProductManager : EntityManager<Product>, IProductService
    {
        public ProductManager(IProductDal repostory) : base(repostory) { }
        public override Product Get(Expression<Func<Product, bool>> filter, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.Get(filter, navigations);
        }

        public Product GetAllLanguageProduct(Expression<Func<Product, bool>> filter, params string[] navigations)
        {
            return base.Get(filter, navigations);
        }

        public override ICollection<Product> GetMany(Expression<Func<Product, bool>> filter = null, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.GetMany(filter, navigations);
        }
        private Expression<Func<Product, bool>> LanguageControl(Expression<Func<Product, bool>> filter = null)
        {
            if (LanguageContext.CurrentLanguage != null && filter != null)
            {
                Expression<Func<Product, bool>> LangEx = x => x.Languageid == LanguageContext.CurrentLanguage.id;
                return filter.And(LangEx);
            }
            return filter;
        }
    }
}
