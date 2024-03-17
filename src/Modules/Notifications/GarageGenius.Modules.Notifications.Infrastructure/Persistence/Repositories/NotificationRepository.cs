using GarageGenius.Modules.Notifications.Core.Entities;
using GarageGenius.Modules.Notifications.Core.Repositories;
using GarageGenius.Modules.Notifications.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Notifications.Infrastructure.Persistence.Repositories;
internal class NotificationRepository : INotificationRepository
{
	private readonly NotificationsDbContext _notificationsDbContext;

	public NotificationRepository(NotificationsDbContext notificationsDbContext)
	{
		_notificationsDbContext = notificationsDbContext;
	}

	public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(Guid userId, CancellationToken cancellationToken = default)
	{
		return await _notificationsDbContext.Notifications
			.Where(n => n.UserId == userId && n.Status == NotificationStatus.Pending)
			.ToListAsync(cancellationToken)
			.ConfigureAwait(false);
	}

	public async Task<Notification> ReadNotificationAsync(Notification notification, CancellationToken cancellationToken = default)
	{
		var dbNotification = await _notificationsDbContext.Notifications
			.Where(n => n.Id == notification.Id)
			.FirstOrDefaultAsync(cancellationToken)
			.ConfigureAwait(false);

		if (dbNotification == null)
			return notification; // TODO not found ?

		dbNotification.MarkAsRead();

		await _notificationsDbContext.SaveChangesAsync(cancellationToken)
			.ConfigureAwait(false);

		return dbNotification;
	}
}
