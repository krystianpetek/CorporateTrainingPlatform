using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Reservations.Core.Reservations.Exceptions;
internal sealed class UnableChangeReservationStateException : GarageGeniusException
{
	public UnableChangeReservationStateException(Guid id) : base($"Unable to change reservation state with ID: {id}, because reservation already completed or canceled.") { }
}
