using System.Linq.Expressions;

namespace Framework.Core.Domain.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }

    // Includes بر اساس Expression (ایمن در برابر Rename)
    List<Expression<Func<T, object>>> Includes { get; }

    // سفارش‌دهی
    List<(Expression<Func<T, object>> KeySelector, bool Desc)> OrderBy { get; }

    // Paging
    int? Skip { get; }
    int? Take { get; }

    // Tracking
    bool AsNoTracking { get; }
}