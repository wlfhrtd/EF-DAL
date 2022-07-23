using System;
using System.Collections.Generic;


namespace Dal.Repository.Base
{
    public interface IRepository<T> : IDisposable
    {
        int Add(T entity, bool persist = true);
        int AddRange(IEnumerable<T> entities, bool persist = true);
        int Update(T entity, bool persist = true);
        int UpdateRange(IEnumerable<T> entities, bool persist = true);
        int Remove(int id, byte[] timeStamp, bool persist = true);
        int Remove(T entity, bool persist = true);
        int RemoveRange(IEnumerable<T> entities, bool persist = true);
        T? FindOneById(int? id);
        T? FindOneByIdAsNoTracking(int id);
        T? FindOneByIdIgnoreQueryFilters(int id);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindAllIgnoreQueryFilters();
        void ExecuteQuery(string sql, object[] sqlParametersObjects);
        int SaveChanges();
    }
}
