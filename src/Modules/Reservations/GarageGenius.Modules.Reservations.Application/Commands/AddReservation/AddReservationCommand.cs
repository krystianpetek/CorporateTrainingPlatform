using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace GarageGenius.Modules.Reservations.Application.Commands.AddReservation;
public record AddReservationCommand : ICommand
{
	[Required]
	public Guid VehicleId { get; init; }

	[Required]
	public Guid CustomerId { get; init; }

    [Required]
    [MaxLength(2000)]
    public string ReservationNote { get; init; }
}
