using GarageGenius.Modules.Reservations.Application.Commands.AddReservation;
using GarageGenius.Modules.Reservations.Application.Events;
using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Exceptions;
using GarageGenius.Modules.Reservations.Core.Reservations.Repositories;
using GarageGenius.Modules.Reservations.Core.Reservations.Services;
using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.MessageBroker;
using Serilog;

namespace GarageGenius.Modules.Reservations.Application.Commands.UpdateReservation;
internal class UpdateReservationCommandHandler : ICommandHandler<UpdateReservationCommand>
{
    private readonly ILogger _logger;
    private readonly IReservationRepository _reservationRepository;
    private readonly IReservationDomainService _reservationDomainService;
    private readonly IMessageBroker _messageBroker;

    public UpdateReservationCommandHandler(
        Serilog.ILogger logger, 
        IReservationRepository reservationRepository,
        IReservationDomainService reservationDomainService,
        IMessageBroker messageBroker)
    {
        _logger = logger;
        _reservationRepository = reservationRepository;
        _reservationDomainService = reservationDomainService;
        _messageBroker = messageBroker;
    }

    public async Task HandleCommandAsync(UpdateReservationCommand command, CancellationToken cancellationToken = default)
    {
        Reservation reservation = await _reservationRepository.GetReservationAsync(command.ReservationId, cancellationToken) ?? throw new ReservationNotFoundException(command.ReservationId);
        await _reservationDomainService.UpdateReservation(reservation, command.ReservationState, command.ReservationNote, cancellationToken);

        _logger.Information(
            messageTemplate: "Command {CommandName} handled by {ModuleName} module, updated reservation with ID: {ReservationId}",
            nameof(UpdateReservationCommand),
            nameof(Reservations),
            command.ReservationId);

        await _messageBroker.PublishAsync(new ReservationUpdatedEvent(reservation.ReservationId, reservation.ReservationNote), cancellationToken);

        _logger.Information(
            messageTemplate: "Event {EventName} published by {ModuleName} module, reservation with ID: {ReservationId} updated",
            nameof(ReservationUpdatedEvent),
            nameof(Reservations),
            reservation.ReservationId);

    }
}
