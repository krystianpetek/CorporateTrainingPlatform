using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Users.Application.Commands.SignIn;

public record SignInCommand : ICommand
{
	[Required]
	[EmailAddress]
	[DefaultValue("krystianpetek2@gmail.com")]
	public string Email { get; init; }

	[Required]
	[DefaultValue("Password!23")]
	public string Password { get; init; }

	public SignInCommand(string email, string password)
	{
		Email = email;
		Password = password;
	}
}