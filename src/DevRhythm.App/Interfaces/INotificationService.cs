using DevRhythm.App.DTOs;

namespace DevRhythm.App.Interfaces
{
    public interface INotificationService
    {
        void SendNotification(NotificationDto notification);
        Task AddNotificationToStorageAsync(NotificationDto notification);
        Task<ICollection<NotificationDto>> GetNotificationsByUserIdAsync(long userId);
    }
}
