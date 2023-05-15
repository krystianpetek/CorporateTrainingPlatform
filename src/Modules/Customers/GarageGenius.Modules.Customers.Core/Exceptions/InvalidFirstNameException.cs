using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Customers.Core.Exceptions;
internal sealed class InvalidFirstNameException : GarageGeniusException
{
    public InvalidFirstNameException(string firstName) : base($"Invalid first name: {firstName}, length should have less than 50 characters.") { }
}