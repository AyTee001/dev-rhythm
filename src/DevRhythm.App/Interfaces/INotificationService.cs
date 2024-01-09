using DevRhythm.App.DTOs;

namespace DevRhythm.App.Interfaces
{
    public interface INotificationService
    {
        void SendNotification(NotificationDto notification);
        Task AddNotificationToStorageAsync(NotificationDto notification);
        Task<IReadOnlyList<NotificationDto>> GetNotificationsByUserIdAsync(long userId);
        Task<int> CountUnreadNotificationsForUserAsync(long userId);
        Task MarkNotificationAsReadByIdAsync(long notificationId, long userId);
        Task MarkNotificationsAsReadAsync(long userId);
    }
}
