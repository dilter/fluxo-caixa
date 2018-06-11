using System;
using System.Linq.Expressions;

namespace Stone.Sdk.Domain.Specification
{
    public class OrOperator<T> : Specification<T>
    {
        private readonly ISpecification<T> _leftSpecification;
        private readonly ISpecification<T> _rightSpecification;

        public OrOperator(ISpecification<T> leftSpecification, ISpecification<T> rightSpecification)
        {
            _leftSpecification = leftSpecification;
            _rightSpecification = rightSpecification;
        }

        public override Expression<Func<T, bool>> IsSatisfiedBy()
        {
            Expression<Func<T, bool>> left = _leftSpecification.IsSatisfiedBy();
            Expression<Func<T, bool>> right = _rightSpecification.IsSatisfiedBy();

            return (left.Or(right));
        }
    }
}