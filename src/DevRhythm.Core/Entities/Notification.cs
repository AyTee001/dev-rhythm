using DevRhythm.Core.Entities.Base;
using DevRhythm.Core.Enums;

namespace DevRhythm.Core.Entities
{
    public class Notification : Entity
    {
        public long? SenderId { get; set; }
        public string? Message { get; set; }
        public DateTime SentAt { get; set; }
        public User? Sender { get; set; }
        public long? PostId { get; set; }
        public Post? Post { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
