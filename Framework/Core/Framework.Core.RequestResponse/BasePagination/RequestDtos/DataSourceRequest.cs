namespace Framework.Core.RequestResponse.BasePagination.RequestDtos;

public class DataSourceRequest
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public List<SortOption> SortOptions { get; set; } = new List<SortOption>() { new SortOption { Field = "Id", IsAscending = false } };
    public List<FilterOption> FilterOptions { get; set; } = new List<FilterOption>();
}