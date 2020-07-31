using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Petroteks.Bll.Helpers.LanguageContext;

namespace Petroteks.Bll.Concreate
{
    public class UI_NoticeManager : EntityManager<UI_Notice>, IUI_NoticeService
    {
        public UI_NoticeManager(IUI_NoticeDal repostory) : base(repostory)
        {
        }
        public override UI_Notice Get(Expression<Func<UI_Notice, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.Get(filter, LangId, navigations);
        }
        public override ICollection<UI_Notice> GetMany(Expression<Func<UI_Notice, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.GetMany(filter, LangId, navigations);
        }

        public ICollection<UI_Notice> LanguageAndWebsiteFilteredData(int websiteId, int languageId)
        {
            return base.GetMany(x => x.IsActive && x.WebSiteid == websiteId && x.Languageid == languageId);
        }
    }
}
