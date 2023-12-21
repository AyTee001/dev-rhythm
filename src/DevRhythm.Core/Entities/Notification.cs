using DevRhythm.Core.Entities.Base;

namespace DevRhythm.Core.Entities
{
    public class Notification : Entity
    {
        public long? SenderId { get; set; }
        public string? Message { get; set; }
        public DateTime SentAt { get; set; }
        public DevRhythmUser? Sender { get; set; }
    }
}
