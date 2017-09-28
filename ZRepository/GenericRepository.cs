using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ZRepository.Interfaces;

namespace ZRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IList<TEntity> GetAllRecords()
        {
            return _dbSet.ToList();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public TEntity GetFirstOrDefault(Int32 recordId)
        {
            return _dbSet.Find(recordId);
        }

        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public IList<TEntity> Where(Func<TEntity, bool> query)
        {
            return _dbSet.Where(query).ToList();
        }

        public IList<TEntity> GetRecordsByPage(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}