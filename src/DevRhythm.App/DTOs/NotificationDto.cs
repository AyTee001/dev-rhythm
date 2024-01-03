using DevRhythm.Core.Enums;

namespace DevRhythm.App.DTOs
{
    public class NotificationDto
    {
        public long? ReceiverId { get; set; }
        public string? Message { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public UserNotificationDto? Sender { get; set; }
        public PostNotificationDto? PostDto { get; set; }
    }
}
