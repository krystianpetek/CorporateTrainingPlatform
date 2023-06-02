using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GarageGenius.Modules.Reservations.Application.Commands.CompleteReservation;
public record CompleteReservationCommand : ICommand
{
	[JsonIgnore]
	public Guid ReservationId { get; set; }

	[DefaultValue("Reservation completed")]
	public string? ReservationResultNote { get; init; }

	public CompleteReservationCommand() { }

	public CompleteReservationCommand(Guid ReservationId, string ReservationResultNote)
	{
		this.ReservationId = ReservationId;
		this.ReservationResultNote = ReservationResultNote;
	}

}
