using System;
using System.Linq;
using System.Linq.Expressions;

namespace Stone.Sdk.Persistence
{
    public interface IUnitOfWork : IDisposable
    {        
        TEntity FindById<TEntity>(Guid id) where TEntity : Entity;     
        IQueryable<TEntity> FindAll<TEntity>() where TEntity : Entity;        
        IQueryable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity;               
        TEntity Add<TEntity>(TEntity entity) where TEntity : Entity;                        
        void Delete<TEntity>(Guid id) where TEntity : Entity;        
        void Commit();
        void Rollback();
    }
}