using System;
using System.Collections.Generic;

namespace ZRepository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IList<TEntity> GetAllRecords();
        void Add(TEntity entity);
        void Update(TEntity entity);
        TEntity GetFirstOrDefault(Int32 recordId);
        void Delete(TEntity entity);
        IList<TEntity> Where(Func<TEntity, bool> query);
        IList<TEntity> GetRecordsByPage(int pageNumber, int pageSize);
    }
}