using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Users.Core.Exceptions;
internal class MissingPasswordException : GarageGeniusException
{
    public MissingPasswordException() : base($"Invalid password") { }
}
