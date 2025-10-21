using System.Linq.Expressions;

namespace Framework.Core.Domain.Specifications;

public sealed class NotSpecification<T> : Specification<T>
{
    public NotSpecification(ISpecification<T> inner)
    {
        if (inner.Criteria is null) return;
        var param = Expression.Parameter(typeof(T), "x");
        var body = Expression.Not(Expression.Invoke(inner.Criteria, param));
        Criteria = Expression.Lambda<Func<T, bool>>(body, param);
    }
}