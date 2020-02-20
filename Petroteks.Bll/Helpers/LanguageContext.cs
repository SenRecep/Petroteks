using Petroteks.Entities.ComplexTypes;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Petroteks.Bll.Helpers
{
    public static class LanguageContext
    {
        public static Language  CurrentLanguage { get; set; }


        public static Expression<Func<T, bool>> LanguageControl<T>(Expression<Func<T, bool>> filter = null) where T:ML_WebsiteObject
        {
            if (CurrentLanguage != null && filter != null)
            {
                Expression<Func<T, bool>> LangEx = x => x.Languageid == CurrentLanguage.id;
                return filter.And(LangEx);
            }
            return filter;
        }
    }
}
