using DevRhythm.App.DTOs;
using DevRhythm.Core.Enums;

namespace DevRhythm.App.Interfaces
{
    public interface INotificationService
    {
        void SendNotification(NotificationDto notification);
        Task AddNotificationToStorageAsync(NotificationDto notification);
        Task<FullNotificationUserDataDto> GetNotificationsByUserIdAsync(long userId);
        Task<int> CountUnreadNotificationsForUserAsync(long userId);
        Task MarkNotificationAsReadByIdAsync(long notificationId, long userId);
        Task MarkNotificationsAsReadAsync(long userId);
        void SetCleanAllNotificationByUserIdJob(long userId, NotificationCleaningPeriod notificationCleaningPeriod);
        Task RemoveNotificationsByUserIdAsync(long userId);
    }
}
