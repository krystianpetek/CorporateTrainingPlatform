using GarageGenius.Modules.Notifications.Core.Entities;
using GarageGenius.Modules.Notifications.Core.Repositories;
using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Notifications.Application.Queries.GetUserNotifications;

public class GetUserNotificationsHandler : IQueryHandler<GetUserNotificationsQuery, GetUserNotificationsDto>
{
    private readonly INotificationRepository _notificationRepository;

    public GetUserNotificationsHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<GetUserNotificationsDto> HandleQueryAsync(GetUserNotificationsQuery query, CancellationToken cancellationToken = default)
    {
        var notifications = await _notificationRepository.GetUserNotificationsAsync(query.UserId);
        var notificationsDto = notifications.Select(notification => (GetUserNotificationDto)notification);

        return new GetUserNotificationsDto(notificationsDto);
    }
}