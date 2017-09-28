using System;

namespace ZRepository.Interfaces
{
    public interface IGenericUnitOfWork
    {
        Type TheType { get; set; }

        GenericRepository<TEntityType> GetRepositoryInstance<TEntityType>() where TEntityType : class;

        void SaveChanges();
    }
}