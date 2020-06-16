using Petroteks.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Petroteks.Bll.Abstract
{
    public interface LanguageEntityManager<T> where T : EntityBase, new()
    {
        T Get(Expression<Func<T, bool>> filter,int LangId, params string[] navigations);
        ICollection<T> GetAll(int LangId, params string[] navigations);
        ICollection<T> GetMany(Expression<Func<T, bool>> filter , int LangId , params string[] navigations);
    }
}
