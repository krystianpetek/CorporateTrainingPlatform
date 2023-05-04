using GarageGenius.Modules.Customers.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GarageGenius.Modules.Customers.Core.ValueObjects;
public class PhoneNumber
{
    public string Value { get; }

    public PhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new InvalidPhoneNumberException(phoneNumber);
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
}