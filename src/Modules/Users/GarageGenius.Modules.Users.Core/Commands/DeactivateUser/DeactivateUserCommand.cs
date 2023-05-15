using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using GarageGenius.Shared.Abstractions.Commands;

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