using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Notifications.Application.Queries.GetUserNotifications;

public class GetUserNotificationsQuery(Guid userId) : IQuery<GetUserNotificationsDto>
{
    public Guid UserId { get; } = userId;
};