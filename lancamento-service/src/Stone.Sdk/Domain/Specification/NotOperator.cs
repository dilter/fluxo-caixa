using System;
using System.Linq;
using System.Linq.Expressions;

namespace Stone.Sdk.Domain.Specification
{
    public class NotOperator<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _specification;

        public NotOperator(ISpecification<T> specification)
        {
            _specification = specification.IsSatisfiedBy();
        }

        public override Expression<Func<T, bool>> IsSatisfiedBy()
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(_specification.Body), _specification.Parameters.Single());
        }
    }
}