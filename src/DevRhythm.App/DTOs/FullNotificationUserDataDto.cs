using DevRhythm.Core.Enums;

namespace DevRhythm.App.DTOs
{
    public class FullNotificationUserDataDto
    {
        public long UserId { get; set; }
        public NotificationCleaningPeriod NotificationCleaningPeriod { get; set; }
        public ICollection<NotificationDto>? Notifications { get; set; }

    }
}
