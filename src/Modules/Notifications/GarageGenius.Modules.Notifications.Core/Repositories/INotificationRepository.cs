using GarageGenius.Modules.Notifications.Core.Entities;

namespace GarageGenius.Modules.Notifications.Core.Repositories;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetUserNotificationsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Notification> ReadNotificationAsync(Notification notification, CancellationToken cancellationToken = default);
    // Task<Notification> GetNotificationAsync(Guid notificationId);
    // Task<Notification> CreateNotificationAsync(Notification notification);
    // Task DeleteNotificationAsync(Guid notificationId);
}