namespace Framework.Core.Domain.ValueObjects;

public class BusinessId : BaseValueObject<BusinessId>
{
    public static BusinessId FromString(string value) => new(value);
    public static BusinessId FromUlid(Ulid value) => new() { Value = value };

    public BusinessId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            //throw new InvalidValueObjectStateException("ValidationErrorIsRequire", nameof(BusinessId));
        }
        if (Ulid.TryParse(value, out Ulid tempValue))
        {
            Value = tempValue;
        }
        else
        {
            //throw new InvalidValueObjectStateException("ValidationErrorInvalidValue", nameof(BusinessId));
        }
    }
    private BusinessId()
    {

    }

    public Ulid Value { get; private set; }

    public override string ToString()
    {
        return Value.ToString();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static explicit operator string(BusinessId title) => title.Value.ToString();
    public static implicit operator BusinessId(string value) => new(value);


    public static explicit operator Ulid(BusinessId title) => title.Value;
    public static implicit operator BusinessId(Ulid value) => new() { Value = value };
}
