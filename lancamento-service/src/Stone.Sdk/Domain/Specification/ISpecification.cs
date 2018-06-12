using System;
using System.Linq.Expressions;

namespace Stone.Sdk.Domain.Specification
{
    public interface ISpecification<T>        
    {
        Expression<Func<T, bool>> IsSatisfiedBy();

        ISpecification<T> And(ISpecification<T> specification);
        
        ISpecification<T> Or(ISpecification<T> specification);

        ISpecification<T> Not(ISpecification<T> specification);
    }
}