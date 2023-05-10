using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Users.Core.Exceptions;
internal class MissingPasswordException : GarageGeniusException
{
    public MissingPasswordException() : base($"Invalid password") { }
}

internal class InvalidCredentialsException : GarageGeniusException
{
    public InvalidCredentialsException() : base("Invalid credentials.") { }
}

internal class InvalidEmailException : GarageGeniusException
{
    public string Email { get; }
    public InvalidEmailException(string email) : base($"Invalid email: {email}")
    {
        Email = email;
    }
}
