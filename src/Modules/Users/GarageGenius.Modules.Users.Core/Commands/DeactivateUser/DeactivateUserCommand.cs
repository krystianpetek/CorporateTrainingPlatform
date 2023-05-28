using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace GarageGenius.Modules.Users.Core.Commands.DeactivateUser;
public record DeactivateUserCommand : ICommand
{
	[Required]
	public Guid UserId { get; init; }

	public DeactivateUserCommand(Guid userId)
	{
		UserId = userId;
	}
}