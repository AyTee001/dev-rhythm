using DevRhythm.App.DTOs;

namespace DevRhythm.Infrastructure.Hubs.Interfaces
{
    public interface INotificationHub
    {
        Task SendNotificationAsync(NotificationDto notification);
    }
}
