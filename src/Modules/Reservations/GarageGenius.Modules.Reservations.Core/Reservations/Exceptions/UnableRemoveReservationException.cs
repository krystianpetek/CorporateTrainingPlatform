using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Reservations.Core.Reservations.Exceptions;
internal sealed class UnableRemoveReservationException : GarageGeniusException
{
    public UnableRemoveReservationException(Guid id) : base($"Unable to remove reservation with ID: {id} because its in progress.") { }
}
