using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Notifications.Core.Entities;

public class Notification : AuditableEntity
{
    public Notification() { }

    public Notification(string userId, string message, NotificationStatus status)
    {
        Id = Guid.NewGuid();
        UserId = Guid.Parse(userId);
        Message = message;
        Status = status;
    }

    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string Message { get; init; }
    public NotificationStatus Status { get; private set; }

    public void MarkAsRead()
    {
        Status = NotificationStatus.Read;
    }
}

public enum NotificationStatus
{
    Pending,
    Sent,
    Failed,
    Read
}