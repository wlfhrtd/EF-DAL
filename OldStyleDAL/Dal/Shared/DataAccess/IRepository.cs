using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Dal.Shared.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        TEntity FindOneByKey(object key);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        IEnumerable<TEntity> Query(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int page = 1,
            int pageSize = 0,
            params Expression<Func<TEntity, object>>[] includedProperties);
    }
}
