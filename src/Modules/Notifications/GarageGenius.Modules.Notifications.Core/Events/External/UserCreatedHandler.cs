using GarageGenius.Modules.Notifications.Core.Services;
using GarageGenius.Modules.Users.Shared.Events;
using GarageGenius.Shared.Abstractions.Events;
using GarageGenius.Shared.Abstractions.Services;
using GarageGenius.Shared.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GarageGenius.Modules.Notifications.Core.Events.External;
internal sealed class UserCreatedHandler : IEventHandler<UserCreated>
{
	private readonly Serilog.ILogger _logger;
	private readonly IHubContext<NotificationsHub> _hubContextNotifications;
	private readonly IEmailSenderService _emailSenderService;
	private readonly ICurrentUserService _currentUserService;

	public UserCreatedHandler(
		Serilog.ILogger logger,
		IEmailSenderService emailSenderService,
		ICurrentUserService currentUserService,
		IHubContext<NotificationsHub> hubContextNotifications)
	{
		_logger = logger;
		_emailSenderService = emailSenderService;
		_currentUserService = currentUserService;
		_hubContextNotifications = hubContextNotifications;
	}

	public async Task HandleEventAsync(UserCreated @event, CancellationToken cancellationToken = default)
	{
		await _hubContextNotifications.Clients.All.SendAsync("SendNotification",  DateTime.Now, @event.Email, cancellationToken);
		await _emailSenderService.SendEmailAsync(_currentUserService.UserId, "Account created","Account created successfully");

		_logger.Information(
			messageTemplate: "Event {EventName} handled by {ModuleName} module, added customer with user ID: {UserId}",
			nameof(UserCreated),
			nameof(Notifications),
			@event.UserId);
	}
}