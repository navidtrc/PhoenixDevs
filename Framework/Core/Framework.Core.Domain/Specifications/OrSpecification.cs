using System.Linq.Expressions;

namespace Framework.Core.Domain.Specifications;

public sealed class OrSpecification<T> : Specification<T>
{
    public OrSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        if (left.Criteria is null && right.Criteria is null) return;

        if (left.Criteria is null) { Criteria = right.Criteria; return; }
        if (right.Criteria is null) { Criteria = left.Criteria; return; }

        var param = Expression.Parameter(typeof(T), "x");
        var body = Expression.OrElse(
            Expression.Invoke(left.Criteria, param),
            Expression.Invoke(right.Criteria, param));
        Criteria = Expression.Lambda<Func<T, bool>>(body, param);
    }
}