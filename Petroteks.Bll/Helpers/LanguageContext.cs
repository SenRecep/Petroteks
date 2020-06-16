using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Petroteks.Bll.Helpers
{
    public static class LanguageContext
    {
        public static ICollection<Language> WebsiteLanguages { get; set; }


        public static Expression<Func<T, bool>> LanguageControl<T>(Expression<Func<T, bool>> filter ,int LangId) where T : ML_WebsiteObject
        {
            if (  filter != null)
            {
                Expression<Func<T, bool>> LangEx = x => x.Languageid == LangId;
                return filter.And(LangEx);
            }
            return filter;
        }
    }
}
