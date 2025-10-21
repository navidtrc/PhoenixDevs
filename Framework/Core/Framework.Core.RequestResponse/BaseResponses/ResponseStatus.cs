using System.Text.RegularExpressions;

namespace Framework.Core.RequestResponse.BaseResponses;

public record ResponseStatus
{
    private static readonly List<ResponseStatus> _statuses = [];
    public static IReadOnlyList<ResponseStatus> Statuses => _statuses.AsReadOnly();

    public int Code { get;  set; }
    public string Value { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    static ResponseStatus()
    {
        _statuses = typeof(ResponseStatus)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy)
            .Where(f => f.FieldType == typeof(ResponseStatus))
            .Select(f => (ResponseStatus)f.GetValue(null)!)
            .ToList();
    }

    public static List<ResponseStatus> Get<TResponseStatus>() where TResponseStatus : ResponseStatus
    {
        return typeof(TResponseStatus)
         .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy)
         .Where(f => typeof(ResponseStatus).IsAssignableFrom(f.FieldType))
         .Select(f => f.GetValue(null) as ResponseStatus)
         .Where(f => f != null)
         .ToList()!;
    }

    public ResponseStatus(int code, string value)
    {
        Code = code;
        Value = value;
        Description = SplitCamelCase(value);
    }

    public ResponseStatus() { }

    public override string ToString() => Value;

    private static string SplitCamelCase(string source) => Regex.Replace(source, "(\\B[A-Z])", " $1");

    public override int GetHashCode() => HashCode.Combine(Code, Value);

    public static ResponseStatus? GetByCode(int code) => _statuses.FirstOrDefault(s => s.Code == code);

    public static ResponseStatus? GetByValue(string value) => _statuses.FirstOrDefault(s => s.Value == value);


    public static implicit operator bool(ResponseStatus status) => status == Success;

    //(General)
    public readonly static ResponseStatus Success = new(1, nameof(Success));
    public readonly static ResponseStatus Consistent = new(2, nameof(Consistent));

    public readonly static ResponseStatus Failed = new(100, nameof(Failed));
    public readonly static ResponseStatus UnknownError = new(101, nameof(UnknownError));
    public readonly static ResponseStatus ServiceUnavailable = new(102, nameof(ServiceUnavailable));
    public readonly static ResponseStatus OperationCanceled = new(103, nameof(OperationCanceled));

    //(Validation)
    public readonly static ResponseStatus BadRequest = new(200, nameof(BadRequest));
    public readonly static ResponseStatus ValidationError = new(201, nameof(ValidationError));
    public readonly static ResponseStatus RequiredFieldMissing = new(202, nameof(RequiredFieldMissing));
    public readonly static ResponseStatus InvalidFormat = new(203, nameof(InvalidFormat));
    public readonly static ResponseStatus OutOfRange = new(204, nameof(OutOfRange));
    public readonly static ResponseStatus TooLong = new(205, nameof(TooLong));
    public readonly static ResponseStatus TooShort = new(206, nameof(TooShort));
    public readonly static ResponseStatus NotMatch = new(207, nameof(NotMatch));
    public readonly static ResponseStatus InvalidEmail = new(208, nameof(InvalidEmail));
    public readonly static ResponseStatus InvalidPhoneNumber = new(209, nameof(InvalidPhoneNumber));
    public readonly static ResponseStatus PhoneNumberRequired = new(210, nameof(PhoneNumberRequired));
    public readonly static ResponseStatus InvalidInputLanguage = new(211, nameof(InvalidInputLanguage));
    public readonly static ResponseStatus InvalidCode = new(212, nameof(InvalidCode));

    //(Authentication & Authorization)
    public readonly static ResponseStatus AuthenticationFailed = new(300, nameof(AuthenticationFailed));
    public readonly static ResponseStatus UnauthorizedAccess = new(301, nameof(UnauthorizedAccess));
    public readonly static ResponseStatus Forbidden = new(302, nameof(Forbidden));
    public readonly static ResponseStatus InvalidToken = new(303, nameof(InvalidToken));
    public readonly static ResponseStatus ExpiredToken = new(304, nameof(ExpiredToken));
    public readonly static ResponseStatus OtpRequired = new(305, nameof(OtpRequired));
    public readonly static ResponseStatus OtpInvalid = new(306, nameof(OtpInvalid));
    public readonly static ResponseStatus InvalidUser = new(307, nameof(InvalidUser));

    //(Data & Persistence)
    public readonly static ResponseStatus NotFound = new(400, nameof(NotFound));
    public readonly static ResponseStatus AlreadyExists = new(401, nameof(AlreadyExists));
    public readonly static ResponseStatus AlreadyRemoved = new(402, nameof(AlreadyRemoved));
    public readonly static ResponseStatus AlreadyConfirmed = new(403, nameof(AlreadyConfirmed));
    public readonly static ResponseStatus DuplicatedData = new(404, nameof(DuplicatedData));
    public readonly static ResponseStatus InvalidId = new(405, nameof(InvalidId));
    public readonly static ResponseStatus Inconsistent = new(406, nameof(Inconsistent));
    public readonly static ResponseStatus ConcurrencyConflict = new(407, nameof(ConcurrencyConflict));
    public readonly static ResponseStatus DatabaseError = new(408, nameof(DatabaseError));
    public readonly static ResponseStatus NotEnoughData = new(409, nameof(NotEnoughData));

    //(Business Rules)
    public readonly static ResponseStatus NotAllowed = new(500, nameof(NotAllowed));
    public readonly static ResponseStatus LimitExceeded = new(501, nameof(LimitExceeded));
    public readonly static ResponseStatus QuotaExceeded = new(502, nameof(QuotaExceeded));
    public readonly static ResponseStatus TimeIsExpired = new(503, nameof(TimeIsExpired));
    public readonly static ResponseStatus NotEnoughBalance = new(504, nameof(NotEnoughBalance));

    //(External Services)
    public readonly static ResponseStatus ThirdPartyError = new(600, nameof(ThirdPartyError));
    public readonly static ResponseStatus Timeout = new(601, nameof(Timeout));
    public readonly static ResponseStatus NetworkError = new(602, nameof(NetworkError));
    public readonly static ResponseStatus ExternalServiceUnavailable = new(603, nameof(ExternalServiceUnavailable));
}