using DevRhythm.Core.Enums;

namespace DevRhythm.Web.DTOs
{
    public class NotificationCleaningDto
    {
        public long UserId { get; set; }
        public NotificationCleaningPeriod NotificationCleaningPeriod { get; set; }
    }
}
