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
        public override AboutUsObject Get(Expression<Func<AboutUsObject, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.Get(filter, LangId, navigations);
        }
        public override ICollection<AboutUsObject> GetMany(Expression<Func<AboutUsObject, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.GetMany(filter, LangId, navigations);
        }

        public ICollection<AboutUsObject> LanguageAndWebsiteFilteredData(int websiteId, int languageId)
        {
            return base.GetMany(x=> x.IsActive && x.WebSiteid==websiteId && x.Languageid==languageId);
        }
    }
}

