using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Reservations.Core.Reservations.Exceptions;
internal sealed class InvalidReservationStateException : GarageGeniusException
{
    public InvalidReservationStateException(string reservationState) : base($"Reservation state name: '{reservationState}' was not found.") { }
}
