using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Petroteks.Bll.Helpers.LanguageContext;
namespace Petroteks.Bll.Concreate
{
    public class PrivacyPolicyObjectManager : EntityManager<PrivacyPolicyObject>, IPrivacyPolicyObjectService
    {
        public PrivacyPolicyObjectManager(IPrivacyPolicyObjectDal repostory) : base(repostory)
        {
        }
        public override PrivacyPolicyObject Get(Expression<Func<PrivacyPolicyObject, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.Get(filter, LangId, navigations);
        }
        public override ICollection<PrivacyPolicyObject> GetMany(Expression<Func<PrivacyPolicyObject, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.GetMany(filter, LangId, navigations);
        }

        public ICollection<PrivacyPolicyObject> LanguageAndWebsiteFilteredData(int websiteId, int languageId)
        {
            return base.GetMany(x => x.IsActive && x.WebSiteid == websiteId && x.Languageid == languageId);
        }
    }
}

