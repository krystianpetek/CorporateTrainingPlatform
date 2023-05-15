using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Customers.Core.Exceptions;
internal sealed class InvalidLastNameException : GarageGeniusException
{
    public InvalidLastNameException(string lastName) : base($"Invalid last name: {lastName}, length should have less than 50 characters.") { }
}