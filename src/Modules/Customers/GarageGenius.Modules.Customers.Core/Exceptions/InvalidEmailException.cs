using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Customers.Core.Exceptions;
internal sealed class InvalidEmailException : GarageGeniusException
{
    public InvalidEmailException(string email) : base($"Invalid email: {email}") { }
}
