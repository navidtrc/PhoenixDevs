namespace Framework.Core.RequestResponse.BaseResponses;

public class ResponseBase<T>
{
    public T Result { get; set; } = default!;
    public ResponseStatus Status { get; set; }

    public ResponseBase(ResponseStatus status) => Status = status;

    public ResponseBase(T result)
    {
        Result = result;
        Status = ResponseStatus.Success;
    }

    public ResponseBase() {}

    public static implicit operator ResponseBase<T>(ResponseStatus status) => new(status);
    public static implicit operator ResponseBase<T>(T result) => new(result);

    public static implicit operator ResponseStatus(ResponseBase<T> responseBase) => responseBase.Status;
    public static implicit operator T(ResponseBase<T> responseBase) => responseBase.Result;

    public static implicit operator bool(ResponseBase<T> response) => response.Status == ResponseStatus.Success;

    public static ResponseBase<T> Success(T result) => new(result);

    public static ResponseBase<T> Failure(ResponseStatus status) => new(status);

    public static ResponseBase<T> Success() => Success(default!);

    public void Deconstruct(out T result, out ResponseStatus status) => (result, status) = (Result, Status);
}
public class ResponseBase
{
    public ResponseStatus Status { get; private set; }

    public List<ResponseStatus>? Errors { get; set; }

    public ResponseBase(ResponseStatus status, List<ResponseStatus>? validationErrors)
    {
        Status = status;
        Errors = validationErrors;
    }

    private ResponseBase(ResponseStatus status)
    {
        Status = status;
    }

    public static ResponseBase Failure(ResponseStatus status) => new(status);

    public static ResponseBase ValidationFailure(List<ResponseStatus> errors)
        => new(ResponseStatus.ValidationError, errors);

    public static ResponseBase Success() => new(ResponseStatus.Success);

    public static implicit operator ResponseBase(ResponseStatus status) => new(status);

    public static implicit operator ResponseStatus(ResponseBase responseBase) => responseBase.Status;

    public static implicit operator bool(ResponseBase response) => response.Status == ResponseStatus.Success;
}