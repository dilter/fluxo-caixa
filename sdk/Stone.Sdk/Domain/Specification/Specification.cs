using System;
using System.Linq.Expressions;

namespace Stone.Sdk.Domain.Specification
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> IsSatisfiedBy();

        public virtual ISpecification<T> And(ISpecification<T> specification)
        {
            return new AndOperator<T>(this, specification);                
        }

        public virtual ISpecification<T> Or(ISpecification<T> specification)
        {
            return new OrOperator<T>(this, specification);                
        }

        public virtual ISpecification<T> Not(ISpecification<T> specification)
        {
            return new NotOperator<T>(specification);
        }
    }
    
    
}