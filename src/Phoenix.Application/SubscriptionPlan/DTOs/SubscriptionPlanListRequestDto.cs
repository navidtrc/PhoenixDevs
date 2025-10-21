using Framework.Core.RequestResponse.BasePagination.RequestDtos;
using Phoenix.Application.SubscriptionPlan.Queries.GetList;

namespace Phoenix.Application.SubscriptionPlan.DTOs;

public class SubscriptionPlanListRequestDto
{
    public int PageSize { get; init; } = 10;
    public int PageNumber { get; init; } = 1;

    public string? SortBy { get; init; } = "Id";
    public bool IsAscending { get; init; } = false;

    public string? PlanTitle { get; init; }
}

public static class SubscriptionPlanListRequestMapping
{
    private const int MaxPageSize = 100;

    public static SubscriptionPlanGetListQuery ToQuery(this SubscriptionPlanListRequestDto dto)
    {
        var pageSize = dto.PageSize <= 0 ? 10 : Math.Min(dto.PageSize, MaxPageSize);
        var pageNumber = dto.PageNumber <= 0 ? 1 : dto.PageNumber;

        return new SubscriptionPlanGetListQuery
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            SortOptions = BuildSort(dto),
            FilterOptions = BuildFilters(dto)
        };
    }

    private static List<SortOption> BuildSort(SubscriptionPlanListRequestDto dto)
    {
        var sortField = string.IsNullOrWhiteSpace(dto.SortBy) ? "Id" : dto.SortBy.Trim();
        return new List<SortOption>
        {
            new SortOption { Field = sortField, IsAscending = dto.IsAscending }
        };
    }

    private static List<FilterOption> BuildFilters(SubscriptionPlanListRequestDto dto)
    {
        var filters = new List<FilterOption>();

        if (!string.IsNullOrWhiteSpace(dto.PlanTitle))
        {
            filters.Add(new FilterOption
            {
                Field = "Title",                // domain/property name on SubscriptionPlan
                Operator = FilterOperator.Contains, // or Like/StartsWith depending on your infra
                Value = dto.PlanTitle!.Trim()
            });
        }

        return filters;
    }
}