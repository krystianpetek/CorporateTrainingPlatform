using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace GarageGenius.Modules.Reservations.Application.Commands.CompleteReservation;
public record CompleteReservationCommand : ICommand
{
	[Required]
	public Guid ReservationId { get; init; }

	public string? ReservationResultNote { get; init; }

	public CompleteReservationCommand() { }

	public CompleteReservationCommand(Guid ReservationId, string ReservationResultNote)
	{
		this.ReservationId = ReservationId;
		this.ReservationResultNote = ReservationResultNote;
	}

}
