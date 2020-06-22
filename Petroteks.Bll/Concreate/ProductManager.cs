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

        public Product GetAllLanguageProduct(Expression<Func<Product, bool>> filter, params string[] navigations)
        {
            return base.Get(filter, navigations);
        }

        public override Product Get(Expression<Func<Product, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.Get(filter, LangId, navigations);
        }
        public override ICollection<Product> GetMany(Expression<Func<Product, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.GetMany(filter, LangId, navigations);
        }

        private Expression<Func<Product, bool>> LanguageControl(Expression<Func<Product, bool>> filter, int LangId)
        {
            if (filter != null)
            {
                Expression<Func<Product, bool>> LangEx = x => x.Languageid == LangId;
                return filter.And(LangEx);
            }
            return filter;
        }
    }
}
