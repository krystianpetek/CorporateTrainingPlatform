using GarageGenius.Modules.Users.Core.Exceptions;

namespace GarageGenius.Modules.Users.Core.ValueObjects;
internal sealed record Email : IEquatable<Email>
{
    public string Value { get; }
    public Email(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEmailException(value);
        }

        if(value.Length > 100)
        {
            throw new InvalidEmailException(value);
        }
        Value = value;
    }

    public static implicit operator Email(string value)
    {
        return new Email(value);
    }

    public static implicit operator string(Email email)
    {
        return email.Value;
    }
}
