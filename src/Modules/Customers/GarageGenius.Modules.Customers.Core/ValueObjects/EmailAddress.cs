using GarageGenius.Modules.Customers.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace GarageGenius.Modules.Customers.Core.ValueObjects;
public record EmailAddress : IEquatable<EmailAddress>
{
    public string Value { get; }

    public EmailAddress(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new InvalidEmailException(email);
        }

        if (email.Length > 100)
        {
            throw new InvalidEmailException(email);
        }

        if (!new EmailAddressAttribute().IsValid(email))
        {
            throw new InvalidEmailException(email);
        }

        Value = email.ToLowerInvariant();
    }

    public static implicit operator string(EmailAddress email)
    {
        return email.Value;
    }

    public static implicit operator EmailAddress(string email)
    {
        return new EmailAddress(email);
    }

    public override string ToString()
    {
        return Value;
    }
}
