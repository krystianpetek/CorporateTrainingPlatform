using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GarageGenius.Modules.Users.Core.Commands.SignUp;

public record SignUpCommand : ICommand
{
    [Required]
    [EmailAddress]
    [DefaultValue("krystianpetek2@gmail.com")]
    public string Email { get; init; }

    [Required]
    [DefaultValue("Password!23")]
    public string Password { get; init; }

    [DefaultValue("Administrator")]
    public string Role { get; init; }

    public SignUpCommand(string email, string password, string role)
    {
        Email = email;
        Password = password;
        Role = role;
    }
}