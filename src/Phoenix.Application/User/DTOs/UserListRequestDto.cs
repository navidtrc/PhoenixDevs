using Framework.Core.RequestResponse.BasePagination.RequestDtos;
using Phoenix.Application.User.Queries.GetList;

namespace Phoenix.Application.User.DTOs;

public class UserListRequestDto
{
    public int PageSize { get; init; } = 10;
    public int PageNumber { get; init; } = 1;

    public string? SortBy { get; init; } = "Id";
    public bool IsAscending { get; init; } = false;

    public string? Username { get; init; }
    public string? Email { get; init; }
}

public static class UserListRequestMapping
{
    private const int MaxPageSize = 100; 

    public static UserGetListQuery ToQuery(this UserListRequestDto dto)
    {
        var pageSize = dto.PageSize <= 0 ? 10 : Math.Min(dto.PageSize, MaxPageSize);
        var pageNumber = dto.PageNumber <= 0 ? 1 : dto.PageNumber;

        var query = new UserGetListQuery
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            SortOptions = BuildSort(dto),
            FilterOptions = BuildFilters(dto)
        };

        return query;
    }

    private static List<SortOption> BuildSort(UserListRequestDto dto)
    {
        var sortField = string.IsNullOrWhiteSpace(dto.SortBy) ? "Id" : dto.SortBy.Trim();
        return new List<SortOption>
        {
            new SortOption { Field = sortField, IsAscending = dto.IsAscending }
        };
    }

    private static List<FilterOption> BuildFilters(UserListRequestDto dto)
    {
        var filters = new List<FilterOption>();

        if (!string.IsNullOrWhiteSpace(dto.Username))
        {
            filters.Add(new FilterOption
            {
                Field = "Username",
                Operator = FilterOperator.Contains, 
                Value = dto.Username!.Trim()
            });
        }

        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            filters.Add(new FilterOption
            {
                Field = "Email",
                Operator = FilterOperator.Contains,
                Value = dto.Email!.Trim()
            });
        }

        return filters;
    }
}