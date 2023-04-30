using GarageGenius.Shared.Abstractions.Exceptions;
using System.Net;

namespace GarageGenius.Modules.Users.Core.Exceptions;
internal class InvalidEmailException : GarageGeniusException
{
    public string Email { get; }
    public InvalidEmailException(string email) : base($"Invalid email: {email}")
    {
        Email = email;
    }
}
