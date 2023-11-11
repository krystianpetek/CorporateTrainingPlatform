using System.ComponentModel.DataAnnotations;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Users.Application.Commands.DeactivateUser;
public record DeactivateUserCommand : ICommand
{
	[Required]
	public Guid UserId { get; init; }

	public DeactivateUserCommand(Guid userId)
	{
		UserId = userId;
	}
}