using GarageGenius.Modules.Reservations.Core.ReservationHistories.Repositories;
using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Exceptions;
using GarageGenius.Modules.Reservations.Core.Reservations.Repositories;
using GarageGenius.Shared.Abstractions.Commands;
using Serilog;

namespace GarageGenius.Modules.Reservations.Application.Commands.UpdateReservation;
internal class UpdateReservationCommandHandler : ICommandHandler<UpdateReservationCommand>
{
    private readonly ILogger _logger;
    private readonly IReservationRepository _reservationRepository;
    private readonly IReservationHistoryRepository _reservationHistoryRepository;

    public UpdateReservationCommandHandler(
        Serilog.ILogger logger, 
        IReservationRepository reservationRepository, 
        IReservationHistoryRepository reservationHistoryRepository)
    {
        _logger = logger;
        _reservationRepository = reservationRepository;
        _reservationHistoryRepository = reservationHistoryRepository;
    }

    public async Task HandleCommandAsync(UpdateReservationCommand command, CancellationToken cancellationToken = default)
    {
        Reservation reservation = await _reservationRepository.GetReservationAsync(command.ReservationId, cancellationToken) ?? throw new ReservationNotFoundException(command.ReservationId);
        reservation.ChangeState(command.ReservationState);

        // TODO - add reservation history

        await _reservationRepository.UpdateReservationAsync(reservation, cancellationToken);

        _logger.Information(
            messageTemplate: "Command {CommandName} handled by {ModuleName} module, updated reservation with ID: {ReservationId}",
            nameof(UpdateReservationCommand),
            nameof(Reservations),
            command.ReservationId);
    }
}
