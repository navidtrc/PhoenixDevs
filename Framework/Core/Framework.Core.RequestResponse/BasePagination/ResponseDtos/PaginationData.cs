namespace Framework.Core.RequestResponse.BasePagination.ResponseDtos;

public abstract class PaginationData
{
    public int PageSize { get; init; }
    public long TotalCount { get; set; }
    public abstract string Type { get; }
}

public sealed class NormalPagingResponse : PaginationData
{
    public override string Type => "Normal";
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
}

public sealed class CursorPagingResponse<TCursor> : PaginationData
{
    public override string Type => "Cursor";
    public TCursor? Cursor { get; init; }
    public int TotalRecords { get; init; }
    public bool HasNext { get; init; }
}