using System;
using System.Data.Entity;
using ZRepository.Interfaces;

namespace ZRepository
{
    /// <summary>
    /// This is a Generic Unit of Work class for any Database Context.  Use the 
    /// GetRepsitoryInstance() method to get a GenericRepository that
    /// can be used to make Database changes in the same Database Context.
    /// </summary>
    public class GenericUnitOfWork : IGenericUnitOfWork
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">Database Context for this unit of Work.</param>
        public GenericUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// The type.
        /// </summary>
        public Type TheType { get; set; }

        /// <summary>
        /// Get a Generic Repository Instance.
        /// </summary>
        /// <typeparam name="TEntityType">The Type of the Repository to Get (i.e. TableName)</typeparam>
        /// <returns>An instance of the GenericRepository for the type given.</returns>
        public GenericRepository<TEntityType> GetRepositoryInstance<TEntityType>() where TEntityType : class
        {
            return new GenericRepository<TEntityType>(_dbContext);
        }

        /// <summary>
        /// Save changes as a Work Unit.
        /// </summary>
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
