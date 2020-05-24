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
        public override PrivacyPolicyObject Get(Expression<Func<PrivacyPolicyObject, bool>> filter, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.Get(filter, navigations);
        }
        public override ICollection<PrivacyPolicyObject> GetMany(Expression<Func<PrivacyPolicyObject, bool>> filter = null, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.GetMany(filter, navigations);
        }
    }
}

