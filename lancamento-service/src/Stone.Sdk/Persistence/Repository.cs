using System;
using System.Linq;
using Stone.Sdk.Domain;
using Stone.Sdk.Domain.Specification;

namespace Stone.Sdk.Persistence
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {        
        protected readonly IUnitOfWork _unitOfWork;        
        protected Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _unitOfWork.FindAll<TEntity>();
        }

        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification = null)
        {
            var all = this.GetAll();
            if (specification != null)
            {
                all = all.Where(specification.IsSatisfiedBy());
            }            
            return all;
        }

        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                var newEntity = _unitOfWork.Add(entity);
                _unitOfWork.Commit();
                return newEntity;
            }
            catch (Exception e)
            {                
                throw e;
            }                       
        }

        public virtual void Delete(TEntity entity)
        {
            _unitOfWork.Delete<TEntity>(entity.Id);
        }

        public virtual TEntity FindById(Guid id)
        {
            return _unitOfWork.FindById<TEntity>(id);
        }
    }
}