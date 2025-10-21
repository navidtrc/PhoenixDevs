using Framework.Core.Contracts.BaseResponses;
using Framework.Core.RequestResponse.BasePagination.ResponseDtos;

namespace Framework.Core.RequestResponse.BaseResponses;

public class ListResponseBase<T> : IResponseBase
{
    public List<T> Result { get; set; } = [];
    public ResponseStatus Status { get; set; }
    public DataSourceResult<T>? Paging { get; set; }

    public ListResponseBase(ResponseStatus status) => Status = status;

    public ListResponseBase() { }

    public ListResponseBase(List<T> result)
    {
        Result = result;
        Status = ResponseStatus.Success;
    }

    public ListResponseBase(List<T> result, DataSourceResult<T> paging)
    {
        Result = result;
        Status = ResponseStatus.Success;
        Paging = paging;
    }

    public static implicit operator ListResponseBase<T>(ResponseStatus status) => new(status);
    public static implicit operator ResponseStatus(ListResponseBase<T> listResponseBase) => listResponseBase.Status;
    public static implicit operator bool(ListResponseBase<T> response) => response.Status == ResponseStatus.Success;

    public static ListResponseBase<T> Success(List<T> result) => new(result);

    public static ListResponseBase<T> Success(
        List<T> result,
        int pageSize,
        int totalPages,
        int currentPage,
        long totalCount)
        => new(result, DataSourceResult<T>.WithNormal(
            data: result,
            pageSize: pageSize,
            totalPages: totalPages,
            currentPage: currentPage,
            totalCount: totalCount));

    public static ListResponseBase<T> Success<TCursor>(
        List<T> result,
        TCursor cursor,
        int pageSize,
        int totalRecord,
        bool hasNext)
        => new(result, DataSourceResult<T>.WithCursor(
            data: result,
            cursor: cursor,
            pageSize: pageSize,
            totalRecords: totalRecord,
            hasNext: hasNext));

    public static ListResponseBase<T> Success(List<T> result, DataSourceResult<T> paging)
        => new(result, paging);

    public static ListResponseBase<T> Failure(ResponseStatus status) => new(status);

    public static ListResponseBase<T> Success() => Success([]);
}
public class ListResponseBase<T, TId> : IResponseBase
{
    public List<T> Result { get; set; } = [];
    public ResponseStatus Status { get; set; }
    public DataSourceResult<T>? Paging { get; set; }

    public ListResponseBase(ResponseStatus status) => Status = status;

    public ListResponseBase() { }

    public ListResponseBase(List<T> result)
    {
        Result = result;
        Status = ResponseStatus.Success;
    }

    public ListResponseBase(List<T> result, DataSourceResult<T> paging)
    {
        Result = result;
        Status = ResponseStatus.Success;
        Paging = paging;
    }

    public static implicit operator ListResponseBase<T, TId>(ResponseStatus status) => new(status);
    public static implicit operator ResponseStatus(ListResponseBase<T, TId> listResponseBase) => listResponseBase.Status;
    public static implicit operator bool(ListResponseBase<T, TId> response) => response.Status == ResponseStatus.Success;

    public static ListResponseBase<T, TId> Success(List<T> result) => new(result);

    public static ListResponseBase<T, TId> Success(
        List<T> result,
        int pageSize,
        int totalPages,
        int currentPage,
        long totalCount)
        => new(result, DataSourceResult<T>.WithNormal(
            data: result,
            pageSize: pageSize,
            totalPages: totalPages,
            currentPage: currentPage,
            totalCount: totalCount));

    public static ListResponseBase<T, TId> Success<TCursor>(
        List<T> result,
        TCursor cursor,
        int pageSize,
        int totalRecords,
        bool hasNext)
        => new(result, DataSourceResult<T>.WithCursor(
            data: result,
            cursor: cursor,
            pageSize: pageSize,
            totalRecords: totalRecords,
            hasNext: hasNext));

    public static ListResponseBase<T, TId> Success(List<T> result, DataSourceResult<T> paging)
        => new(result, paging);

    public static ListResponseBase<T, TId> Failure(ResponseStatus status) => new(status);

    public static ListResponseBase<T, TId> Success() => Success([]);
}