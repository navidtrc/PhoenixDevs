namespace Framework.Core.RequestResponse.BasePagination.RequestDtos;

public class SortOption
{
    public string Field { get; set; }
    public bool IsAscending { get; set; } = true;
}