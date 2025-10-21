namespace Framework.Core.RequestResponse.BasePagination.ResponseDtos;

public class DataSourceResult<TEntity>
{
    public IEnumerable<TEntity> Data { get; set; }
    public PaginationData Paging { get; set; }
    public static DataSourceResult<TEntity> WithNormal(
        IEnumerable<TEntity> data,
        int pageSize,
        int totalPages,
        int currentPage,
        long totalCount)
    {
        return new DataSourceResult<TEntity>
        {
            Data = data,
            Paging = new NormalPagingResponse
            {
                PageSize = pageSize,
                CurrentPage = currentPage,
                TotalPages = totalPages,
                TotalCount = totalCount
            }
        };
    }
    public static DataSourceResult<TEntity> WithCursor<TCursor>(
        IEnumerable<TEntity> data,
        TCursor cursor,
        int pageSize,
        int totalRecords,
        bool hasNext)
    {
        return new DataSourceResult<TEntity>
        {
            Data = data,
            Paging = new CursorPagingResponse<TCursor>
            {
                Cursor = cursor,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                HasNext = hasNext
            }
        };
    }
}