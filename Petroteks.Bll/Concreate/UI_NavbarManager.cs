using Petroteks.Bll.Abstract;
using Petroteks.Dal.Abstract;
using Petroteks.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Petroteks.Bll.Helpers.LanguageContext;

namespace Petroteks.Bll.Concreate
{
    public class UI_NavbarManager : EntityManager<UI_Navbar>, IUI_NavbarService
    {
        public UI_NavbarManager(IUI_NavbarDal repostory) : base(repostory)
        {
        }
        public override UI_Navbar Get(Expression<Func<UI_Navbar, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.Get(filter, LangId, navigations);
        }
        public override ICollection<UI_Navbar> GetMany(Expression<Func<UI_Navbar, bool>> filter, int LangId, params string[] navigations)
        {
            filter = LanguageControl(filter, LangId);
            return base.GetMany(filter, LangId, navigations);
        }

    }
}
