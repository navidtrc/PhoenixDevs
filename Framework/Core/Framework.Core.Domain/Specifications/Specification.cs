using System.Linq.Expressions;

namespace Framework.Core.Domain.Specifications;

public abstract class Specification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; protected init; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<(Expression<Func<T, object>> KeySelector, bool Desc)> OrderBy { get; } = new();
    public int? Skip { get; protected set; }
    public int? Take { get; protected set; }
    public bool AsNoTracking { get; protected set; } = true;

    protected void AddInclude(Expression<Func<T, object>> include) => Includes.Add(include);

    protected void ApplyOrderBy(Expression<Func<T, object>> keySelector, bool desc = false)
        => OrderBy.Add((keySelector, desc));

    protected void ApplyPaging(int skip, int take) { Skip = skip; Take = take; }

    protected void WithTracking(bool tracking = false) => AsNoTracking = !tracking;
}