using GarageGenius.Modules.Customers.Core.Exceptions;
using System.Text.RegularExpressions;

namespace GarageGenius.Modules.Customers.Core.ValueObjects;
internal sealed class PhoneNumber : IEquatable<PhoneNumber>
{
    public string Value { get; }

    public PhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            // IMPORTANT Phone Number can be empty when creating a customer
            Value = string.Empty;
            return;
            //throw new InvalidPhoneNumberException(phoneNumber);
        }

        if (!Regex.IsMatch(phoneNumber, @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$",
            RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
        {
            throw new InvalidPhoneNumberException(phoneNumber);
        }

        Value = phoneNumber.ToLowerInvariant();
    }

    public static implicit operator string(PhoneNumber phoneNumber)
    {
        return phoneNumber.Value;
    }

    public static implicit operator PhoneNumber(string phoneNumber)
    {
        return new PhoneNumber(phoneNumber);
    }

    public override string ToString()
    {
        return Value;
    }

    public bool Equals(PhoneNumber? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((PhoneNumber?)obj);
    }

    public override int GetHashCode()
    {
        return Value is not null ? Value.GetHashCode() : 0;
    }
}