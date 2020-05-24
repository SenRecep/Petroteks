using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Petroteks.Bll.Helpers.LanguageContext;
namespace Petroteks.Bll.Concreate
{
    public class DynamicPageManager : EntityManager<DynamicPage>, IDynamicPageService
    {
        public DynamicPageManager(IDynamicPageDal repostory) : base(repostory)
        {
        }
        public override DynamicPage Get(Expression<Func<DynamicPage, bool>> filter, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.Get(filter, navigations);
        }
        public override ICollection<DynamicPage> GetMany(Expression<Func<DynamicPage, bool>> filter = null, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.GetMany(filter, navigations);
        }
    }
}

