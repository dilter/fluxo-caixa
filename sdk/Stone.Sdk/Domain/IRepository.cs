using System;
using System.Linq;
using Stone.Sdk.Persistence;

namespace Stone.Sdk.Domain
{
    public interface IRepository<TEntity> where TEntity : Entity
    {        
        IQueryable<TEntity> GetAll();    
        TEntity Add(TEntity entity);        
        void Delete(TEntity entity);
        TEntity FindById(Guid id);        
    }
}
