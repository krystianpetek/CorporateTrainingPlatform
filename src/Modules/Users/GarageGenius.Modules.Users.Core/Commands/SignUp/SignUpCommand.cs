using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace GarageGenius.Modules.Users.Core.Commands.SignUp;

public record SignUpCommand([Required][EmailAddress] string Email, [Required] string Password, string Role) : ICommand;
