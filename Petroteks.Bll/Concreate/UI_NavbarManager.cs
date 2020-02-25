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
        public override UI_Navbar Get(Expression<Func<UI_Navbar, bool>> filter, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.Get(filter, navigations);
        }
        public override ICollection<UI_Navbar> GetMany(Expression<Func<UI_Navbar, bool>> filter = null, params string[] navigations)
        {
            filter = LanguageControl(filter);
            return base.GetMany(filter, navigations);
        }
    }
}
