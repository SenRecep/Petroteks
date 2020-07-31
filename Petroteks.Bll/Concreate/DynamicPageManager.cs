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

        public override DynamicPage Get(Expression<Func<DynamicPage, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.Get(filter, LangId, navigations);
        }
        public override ICollection<DynamicPage> GetMany(Expression<Func<DynamicPage, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.GetMany(filter, LangId, navigations);
        }

        public ICollection<DynamicPage> LanguageAndWebsiteFilteredData(int websiteId, int languageId)
        {
            return base.GetMany(x => x.IsActive && x.WebSiteid == websiteId && x.Languageid == languageId);
        }
    }
}

