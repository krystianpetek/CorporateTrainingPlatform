using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace GarageGenius.Modules.Reservations.Application.Commands.UpdateReservation;
public record UpdateReservationCommand : ICommand
{
	[Required]
	public Guid ReservationId { get; init; }

	[Required]
	public string ReservationState { get; init; }

	[Required]
	[MaxLength(2000)]
	public string ReservationNote { get; init; }

	public DateTime ReservationDate { get; init; }
}
