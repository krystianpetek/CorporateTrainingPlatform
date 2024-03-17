using GarageGenius.Modules.Notifications.Core.Entities;

namespace GarageGenius.Modules.Notifications.Application.Queries.GetUserNotifications;

public record GetUserNotificationDto
{
    public GetUserNotificationDto(Guid id, Guid userId, string message, NotificationStatus status, bool isRead, DateTime createdDate)
    {
        Id = id;
        UserId = userId;
        Message = message;
        Status = status;
        IsRead = isRead;
        CreatedDate = createdDate;
    }

    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string Message { get; init; }
    public NotificationStatus Status { get; init; }
    public bool IsRead { get; init; }
    public DateTime CreatedDate { get; init; }


    public GetUserNotificationDto(Notification notification)
    {
        Id = notification.Id;
        UserId = notification.UserId;
        Message = notification.Message;
        Status = notification.Status;
        IsRead = notification.Status == NotificationStatus.Read;
        CreatedDate = notification.Created;
    }

    // converter from notification to dto
    public static implicit operator GetUserNotificationDto(Notification notification)
    {
        return new GetUserNotificationDto(notification);
    }
}

public record GetUserNotificationsDto
{
    public IEnumerable<GetUserNotificationDto> Notifications { get; init; }

    public GetUserNotificationsDto(IEnumerable<GetUserNotificationDto> notifications)
    {
        Notifications = notifications;
    }
}