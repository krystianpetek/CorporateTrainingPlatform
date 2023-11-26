using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Users.Application.Commands.SignUp;

public record SignUpCommand : ICommand
{
	[Required]
	[EmailAddress]
	[DefaultValue("krystianpetek2@gmail.com")]
	public string Email { get; init; }

	[Required]
	[DefaultValue("Password!23")]
	public string Password { get; init; }

	[DefaultValue("administrator")]
	public string Role { get; init; }

	public SignUpCommand(string email, string password, string role)
	{
		Email = email;
		Password = password;
		Role = role;
	}
}