using GarageGenius.Modules.Reservations.Core.Reservations.Types;
using GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;
using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GarageGenius.Modules.Reservations.Application.Commands.AddReservation;
public record AddReservationCommand : ICommand
{
    [Required]
    public Guid VehicleId { get; init; }

    [Required]
    public string ReservationState { get; init; }

    [Required]
    public DateTime ReservationDate { get; init; }

    [Required]
    [MaxLength(2000)]
    public string ReservationNote { get; init; }
}
