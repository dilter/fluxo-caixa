using System;
using System.Linq;
using Stone.Sdk.Domain.Specification;
using Stone.Sdk.Persistence;

namespace Stone.Sdk.Domain
{
    public interface IRepository<TEntity> where TEntity : Entity
    {        
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> FindAll(ISpecification<TEntity> specification);
        TEntity Add(TEntity entity);        
        void Delete(TEntity entity);
        TEntity FindById(Guid id);        
    }
}
