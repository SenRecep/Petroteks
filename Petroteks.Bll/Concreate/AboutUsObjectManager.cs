using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Petroteks.Bll.Helpers.LanguageContext;
namespace Petroteks.Bll.Concreate
{
    public class AboutUsObjectManager : EntityManager<AboutUsObject>, IAboutUsObjectService
    {
        public AboutUsObjectManager(IAboutUsObjectDal repostory) : base(repostory)
        {
        }
        public override AboutUsObject Get(Expression<Func<AboutUsObject, bool>> filter, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.Get(filter, navigations);
        }
        public override ICollection<AboutUsObject> GetMany(Expression<Func<AboutUsObject, bool>> filter = null, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.GetMany(filter, navigations);
        }
    }
}

