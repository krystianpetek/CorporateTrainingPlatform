using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Customers.Core.Exceptions;
internal sealed class InvalidPhoneNumberException : GarageGeniusException
{
    public InvalidPhoneNumberException(string phoneNumber) : base($"Invalid phone number: {phoneNumber}") { }
}