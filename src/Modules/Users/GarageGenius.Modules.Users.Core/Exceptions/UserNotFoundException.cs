using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Users.Core.Exceptions;
internal class UserNotFoundException : GarageGeniusException
{
    public UserNotFoundException(Guid id) : base($"User with ID: '{id}' was not found.") { }
}
