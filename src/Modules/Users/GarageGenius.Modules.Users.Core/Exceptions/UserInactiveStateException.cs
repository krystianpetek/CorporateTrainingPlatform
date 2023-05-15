using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Users.Core.Exceptions;
internal class UserInactiveStateException : GarageGeniusException
{
    public UserInactiveStateException(Guid id) : base($"User with ID: '{id}' exists in system, but its inactive.") { }
}
