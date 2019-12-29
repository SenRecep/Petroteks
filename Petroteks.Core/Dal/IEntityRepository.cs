using Petroteks.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Petroteks.Core.Dal
{
    public interface IEntityRepostory<T> where T : EntityBase, new()
    {
        T Get(Expression<Func<T, bool>> filter, params string[] navigations);
        ICollection<T> GetAll(params string[] navigations);
        ICollection<T> GetMany(Expression<Func<T, bool>> filter = null, params string[] navigations);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> filter);
        void Save();
        int _Save();
        void Dispose();
        void Build();
    }
}
