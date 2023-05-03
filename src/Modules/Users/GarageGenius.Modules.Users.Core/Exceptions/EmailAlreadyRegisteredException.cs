using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Users.Core.Exceptions;
internal class EmailAlreadyRegisteredException : GarageGeniusException
{
    public EmailAlreadyRegisteredException() : base("Email is already in use.")
    {
    }
}