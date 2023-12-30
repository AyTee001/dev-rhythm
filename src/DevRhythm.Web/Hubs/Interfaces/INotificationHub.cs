using DevRhythm.Core.Entities;

namespace DevRhythm.Infrastructure.Hubs.Interfaces
{
    public interface INotificationHub
    {
        Task SendNotificationAsync(Notification notification);
    }
}
