using Petroteks.Core.Dal;
using Petroteks.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Petroteks.Bll.Concreate
{
    public class EntityManager<T> : IEntityRepostory<T> where T : EntityBase, new()
    {
        public IEntityRepostory<T> _repostory;
        public EntityManager(IEntityRepostory<T> repostory)
        {
            _repostory = repostory;
        }
        public virtual void Add(T entity)
        {
            _repostory.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _repostory.Delete(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> filter)
        {
            _repostory.Delete(filter);
        }

        public virtual T Get(Expression<Func<T, bool>> filter, params string[] navigations)
        {
            return _repostory.Get(filter, navigations);
        }

        public virtual ICollection<T> GetAll(params string[] navigations)
        {
            return _repostory.GetAll(navigations);
        }

        public virtual ICollection<T> GetMany(Expression<Func<T, bool>> filter = null, params string[] navigations)
        {
            return _repostory.GetMany(filter, navigations);
        }

        public virtual void Save()
        {
            _repostory.Save();
        }

        public virtual int _Save()
        {
            return _repostory._Save();
        }

        public virtual void Update(T entity)
        {
            _repostory.Update(entity);
        }

        public void Dispose()
        {
            _repostory.Dispose();
        }

        public void Build()
        {
            _repostory.Build();
        }
    }
}
