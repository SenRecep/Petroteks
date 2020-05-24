using Microsoft.EntityFrameworkCore;
using Petroteks.Core.Dal;
using Petroteks.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Petroteks.Dal.Concreate.EntityFramework
{
    public class EfEntityRepostoryBase<TEntity, TContext> : IEntityRepostory<TEntity>
          where TEntity : EntityBase, new()
          where TContext : DbContext, new()
    {
        private DbContext dbContext = null;
        private readonly DbSet<TEntity> table = null;
        private readonly object LockObj = new object();

        public EfEntityRepostoryBase()
        {
            Build();
            table = dbContext.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> _entity = dbContext.Entry<TEntity>(entity);
            _entity.State = EntityState.Added;
        }

        public virtual void Delete(TEntity entity)
        {
            entity.IsActive = false;
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> _entity = dbContext.Entry<TEntity>(entity);
            _entity.State = EntityState.Modified;
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> entities = table.Where(filter);
            foreach (TEntity entity in entities)
            {
                Delete(entity);
            }
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter, params string[] navigations)
        {
            IQueryable<TEntity> set = table.AsQueryable();
            navigations.ToList().ForEach(item =>
            {
                set.Include(item);
            });
            return set.FirstOrDefault(filter);
        }

        public virtual ICollection<TEntity> GetAll(params string[] navigations)
        {
            IQueryable<TEntity> set = table.AsQueryable();
            navigations.ToList().ForEach(item =>
            {
                set.Include(item);
            });
            return set.ToList();
        }

        public virtual ICollection<TEntity> GetMany(Expression<Func<TEntity, bool>> filter = null, params string[] navigations)
        {
            IQueryable<TEntity> set = table.AsQueryable();
            navigations.ToList().ForEach(item =>
            {
                set.Include(item);
            });
            return filter == null ? set.ToList() : set.Where(filter).ToList();
        }

        public virtual void Save()
        {
            dbContext.SaveChanges();
        }

        public virtual int _Save()
        {
            return dbContext.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> _entity = dbContext.Entry<TEntity>(entity);
            _entity.State = EntityState.Modified;
        }

        public virtual void Dispose()
        {
            dbContext.DisposeAsync();
        }
        public virtual void Build()
        {
            lock (LockObj)
            {
                if (dbContext == null || !(dbContext is TContext))
                {
                    dbContext = new TContext();
                }
            }
        }
    }
}
