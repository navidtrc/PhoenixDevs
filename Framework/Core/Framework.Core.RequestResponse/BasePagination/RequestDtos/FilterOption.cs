namespace Framework.Core.RequestResponse.BasePagination.RequestDtos;

public class FilterOption
{
    public string Field { get; set; }
    public string Value { get; set; }
    public FilterOperator Operator { get; set; } = FilterOperator.Equals;
}