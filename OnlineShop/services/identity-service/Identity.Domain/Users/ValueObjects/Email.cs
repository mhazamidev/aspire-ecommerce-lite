using Domain.SeekWork;
using Domain.SeekWork.Exceptions;
using OnlineShop.Utility;

namespace Identity.Domain.Users.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }


    private Email(string value)
    {
        Value = value;
    }
    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Email is required.");

        if (!value.IsValidEmail())
            throw new DomainException("Invalid email format.");

        return new Email(value.Trim().ToLowerInvariant());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
