using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Petroteks.Bll.Helpers.LanguageContext;

namespace Petroteks.Bll.Concreate
{
    public class UI_FooterManager : EntityManager<UI_Footer>, IUI_FooterService
    {
        public UI_FooterManager(IUI_FooterDal repostory) : base(repostory)
        {
        }
        public override UI_Footer Get(Expression<Func<UI_Footer, bool>> filter, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.Get(filter, navigations);
        }
        public override ICollection<UI_Footer> GetMany(Expression<Func<UI_Footer, bool>> filter = null, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.GetMany(filter, navigations);
        }
    }
}
