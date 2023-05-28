using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Reservations.Core.Reservations.Exceptions;
internal sealed class ReservationNotFoundException : GarageGeniusException
{
	public ReservationNotFoundException(Guid id) : base($"Reservation with ID: '{id}' was not found.") { }
}
