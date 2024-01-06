using DevRhythm.App.DTOs;
using DevRhythm.Shared.Enums;

namespace DevRhythm.Web.Models
{
    public class NotificationsIndexModel
    {
        public long UserId { get; set; }
        public NotificationCleaningPeriod NotificationCleaningPeriod { get; set; }
        public ICollection<NotificationDto>? Notifications { get; set; }
    }
}
