using GarageGenius.Modules.Reservations.Application.Events;
using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Services;
using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.MessageBroker;

namespace GarageGenius.Modules.Reservations.Application.Commands.AddReservation;
internal class AddReservationCommandHandler : ICommandHandler<AddReservationCommand>
{
    private readonly Serilog.ILogger _logger;
    private readonly IMessageBroker _messageBroker;
    private readonly IReservationDomainService _reservationDomainService;

    public AddReservationCommandHandler(
        Serilog.ILogger logger,
        IMessageBroker messageBroker,
        IReservationDomainService reservationDomainService)
    {
        _logger = logger;
        _messageBroker = messageBroker;
        _reservationDomainService = reservationDomainService;
    }

    public async Task HandleCommandAsync(AddReservationCommand command, CancellationToken cancellationToken = default)
    {
        Reservation reservation = new Reservation(command.VehicleId, command.ReservationNote, command.ReservationDate);
        await _reservationDomainService.AddReservation(reservation, cancellationToken);

        _logger.Information(
            messageTemplate: "Command {CommandName} handled by {ModuleName} module, added new reservation with ID: {ReservationId}",
            nameof(AddReservationCommand),
            nameof(Reservations),
            reservation.ReservationId);

        await _messageBroker.PublishAsync(new ReservationAdded(reservation.ReservationId, reservation.ReservationNote), cancellationToken);

        _logger.Information(
            messageTemplate: "Event {EventName} published by {ModuleName} module, reservation with ID: {ReservationId} added",
            nameof(ReservationAdded),
            nameof(Reservations),
            reservation.ReservationId);
    }
}
