using GarageGenius.Modules.Notifications.Core.Services;
using GarageGenius.Modules.Reservations.Shared.Events;
using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Notifications.Core.Events.External;

internal class ReservationAddedHandler : IEventHandler<ReservationAddedEvent>
{
    private readonly Serilog.ILogger _logger;
    private readonly IEmailSenderService _emailSenderService;
    
    public ReservationAddedHandler(
        Serilog.ILogger logger,
        IEmailSenderService emailSenderService)
    {
        _logger = logger;
        _emailSenderService = emailSenderService;
    }

    public async Task HandleEventAsync(ReservationAddedEvent @event, CancellationToken cancellationToken = default)
    {
        await _emailSenderService.SendEmailAsync("krystianpetek2@gmail.com", "Reservation created in GarageGenius", $"Reservation {@event.ReservationId} created successfully");

        _logger.Information(
            messageTemplate: "Event {EventName} handled by {ModuleName} module, added customer with user ID: {UserId}",
            nameof(ReservationAddedEvent),
            nameof(Notifications),
            @event.UserId);
    }
}