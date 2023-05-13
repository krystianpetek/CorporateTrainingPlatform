using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using GarageGenius.Shared.Abstractions.Queries;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GarageGenius.Modules.Users.Core.Queries.SignIn;

public record SignInQuery : IQuery<JsonWebTokenDto>
{
    [Required]
    [EmailAddress]
    [DefaultValue("krystianpetek2@gmail.com")]
    public string Email { get; init; }

    [Required]
    [DefaultValue("Password!23")]
    public string Password { get; init; }

    public SignInQuery(string email, string password)
    {
        Email = email;
        Password = password;
    }
}