using Microsoft.EntityFrameworkCore;
using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Dal.Shared.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal ApplicationDbContext context;

        internal DbSet<TEntity> dbSet;


        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }


        /// <summary>
        /// Sets EntityState.Deleted
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Sets EntityState.Modified
        /// </summary>
        /// <param name="entity">Entity to update</param>
        public virtual void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        /// <summary>
        /// Sets EntityState.Added
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Finds entity with given primary key
        /// </summary>
        /// <param name="key">PK of entity to find</param>
        /// <returns>Instance of entity or null</returns>
        public virtual TEntity FindOneByKey(object key)
        {
            return dbSet.Find(key);
        }

        /// <summary>
        /// Allows query entities
        /// </summary>
        /// <param name="filter">Lambda expr for filtering rows</param>
        /// <param name="orderBy">Lambda expr for sorting</param>
        /// <param name="page">Pagination: return specified page when pageSize > 0</param>
        /// <param name="pageSize">Pagination: number of items per page; 0 returns all data</param>
        /// <param name="includedProperties">Add argument for each property for eager load</param>
        /// <returns>IEnumerable of type or null</returns>
        public virtual IEnumerable<TEntity> Query(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int page = 1,
            int pageSize = 0,
            params Expression<Func<TEntity, object>>[] includedProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var property in includedProperties)
            {
                query.Include(property);
            }

            if (pageSize > 0)
            {
                query = query.Take(pageSize).Skip((page - 1) * pageSize);
            }

            return orderBy == null ? query.ToList() : orderBy(query).ToList();
        }
    }
}
