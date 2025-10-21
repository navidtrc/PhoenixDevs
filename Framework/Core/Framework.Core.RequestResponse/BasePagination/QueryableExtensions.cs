using Framework.Core.Domain.Abstractions;
using Framework.Core.RequestResponse.BasePagination.RequestDtos;
using Framework.Core.RequestResponse.BasePagination.ResponseDtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Framework.Core.RequestResponse.BasePagination;

public static class QueryableExtensions
{
    public static async Task<DataSourceResult<TEntity>> ToDataSourceResultAsync<TEntity>(
        this IQueryable<TEntity> source,
        DataSourceRequest request,
        CancellationToken cancellationToken = default)
        where TEntity : IAuditableEntity
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(request);

        var query = source;

        // Apply filters (if any)
        if (request.FilterOptions is { Count: > 0 })
        {
            foreach (var filter in request.FilterOptions)
                query = ApplyFilter(query, filter);
        }

        // Compute total count AFTER filters, BEFORE paging
        var totalCount = await query.CountAsync(cancellationToken);

        // Apply sorting (if any)
        if (request.SortOptions is { Count: > 0 })
        {
            bool isFirst = true;
            foreach (var sort in request.SortOptions)
            {
                query = ApplySorting(query, sort, isFirst);
                isFirst = false;
            }
        }

        // Normalize pagination inputs
        var pageSize = Math.Max(1, request.PageSize);
        var currentPage = Math.Max(1, request.PageNumber);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        // Apply paging
        var skip = (currentPage - 1) * pageSize;
        var data = await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return DataSourceResult<TEntity>.WithNormal(
            data: data,
            pageSize: pageSize,
            totalPages: totalPages,
            currentPage: currentPage,
            totalCount: totalCount
        );
    }

    public static string GetPaginationMetadata<T>(this DataSourceResult<T> result)
    {
        // Assumes Normal paging for this helper
        if (result.Paging is not NormalPagingResponse n)
            return "{}";

        return Newtonsoft.Json.JsonConvert.SerializeObject(new
        {
            totalCount = n.TotalCount,
            pageSize = n.PageSize,
            currentPage = n.CurrentPage,
            totalPages = n.TotalPages
        });
    }

    // -------------------------
    // Helpers
    // -------------------------

    private static IQueryable<TEntity> ApplyFilter<TEntity>(IQueryable<TEntity> query, FilterOption filter)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = GetNestedPropertyExpression(parameter, filter.Field); // supports "a.b.c"

        // Convert filter.Value to the property type (handles Nullable<T>)
        var targetType = Nullable.GetUnderlyingType(property.Type) ?? property.Type;
        var typedValue = ChangeType(filter.Value, targetType);
        var value = Expression.Constant(typedValue, targetType);
        var left = property.Type != targetType ? (Expression)Expression.Convert(property, targetType) : property;

        Expression predicate = filter.Operator switch
        {
            FilterOperator.Equals      => Expression.Equal(left, value),
            FilterOperator.GreaterThan => Expression.GreaterThan(left, value),
            FilterOperator.LessThan    => Expression.LessThan(left, value),

            // String-only operators
            FilterOperator.Contains    => BuildStringCall(left, "Contains", value),
            FilterOperator.StartsWith  => BuildStringCall(left, "StartsWith", value),
            FilterOperator.EndsWith    => BuildStringCall(left, "EndsWith", value),

            _ => throw new NotSupportedException($"Filter operator '{filter.Operator}' is not supported.")
        };

        var lambda = Expression.Lambda<Func<TEntity, bool>>(predicate, parameter);
        return query.Where(lambda);
    }

    private static MemberExpression GetNestedPropertyExpression(Expression parameter, string propertyPath)
    {
        if (string.IsNullOrWhiteSpace(propertyPath))
            throw new ArgumentException("Property path is required.", nameof(propertyPath));

        Expression current = parameter;
        foreach (var part in propertyPath.Split('.'))
        {
            current = Expression.Property(current, part);
        }
        return (MemberExpression)current;
    }

    private static Expression BuildStringCall(Expression property, string methodName, Expression value)
    {
        var (nullableUnderlying, isNullable) = (Nullable.GetUnderlyingType(property.Type), false);
        if (nullableUnderlying is not null)
        {
            isNullable = true;
            property = Expression.Convert(property, nullableUnderlying);
        }

        if (property.Type != typeof(string))
            throw new NotSupportedException($"Operator '{methodName}' is only supported on string properties.");

        // property != null ? property.Method(value) : false
        var method = typeof(string).GetMethod(methodName, new[] { typeof(string) })!;
        Expression call = Expression.Call(property, method, value);

        if (isNullable)
        {
            var notNull = Expression.NotEqual(Expression.Convert(property, typeof(string)), Expression.Constant(null, typeof(string)));
            call = Expression.Condition(notNull, call, Expression.Constant(false));
        }

        return call;
    }

    private static IQueryable<TEntity> ApplySorting<TEntity>(IQueryable<TEntity> query, SortOption sort, bool isFirst)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = GetNestedPropertyExpression(parameter, sort.Field);
        var lambda = Expression.Lambda(property, parameter);
        var propertyType = property.Type;

        var methodName =
            isFirst
                ? (sort.IsAscending ? "OrderBy" : "OrderByDescending")
                : (sort.IsAscending ? "ThenBy" : "ThenByDescending");

        var methods = typeof(Queryable).GetMethods()
            .Where(m => m.Name == methodName && m.IsGenericMethodDefinition)
            .Select(m => new { m, args = m.GetGenericArguments(), pars = m.GetParameters() });

        var target = methods.First(x => x.args.Length == 2 && x.pars.Length == 2).m
            .MakeGenericMethod(typeof(TEntity), propertyType);

        return (IQueryable<TEntity>)target.Invoke(null, new object[] { query, lambda })!;
    }

    private static object? ChangeType(object? value, Type targetType)
    {
        if (value is null) return null;
        if (targetType.IsEnum)
        {
            if (value is string s) return Enum.Parse(targetType, s, ignoreCase: true);
            return Enum.ToObject(targetType, System.Convert.ChangeType(value, Enum.GetUnderlyingType(targetType))!);
        }
        if (targetType == typeof(Guid))
            return value is Guid g ? g : Guid.Parse(value.ToString()!);
        if (targetType == typeof(DateTime))
            return value is DateTime dt ? dt : DateTime.Parse(value.ToString()!, System.Globalization.CultureInfo.InvariantCulture);
        if (targetType == typeof(bool))
            return value is bool b ? b : bool.Parse(value.ToString()!);

        return System.Convert.ChangeType(value, targetType, System.Globalization.CultureInfo.InvariantCulture);
    }
}