using GarageGenius.Shared.Abstractions.Queries;
using System.ComponentModel.DataAnnotations;

namespace GarageGenius.Modules.Users.Core.Queries.SignIn;
public record SignInQuery([Required][EmailAddress] string Email, [Required] string Password) : IQuery<string>;
