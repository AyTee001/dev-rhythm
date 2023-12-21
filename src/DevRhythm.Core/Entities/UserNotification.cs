namespace DevRhythm.Core.Entities
{
    public class UserNotification
    {
        public long ReceiverId { get; set; }
        public long NotificationId { get; set; }
        public bool IsRead { get; set; }
        public Notification? Notification { get; set; }
        public DevRhythmUser? Receiver { get; set; }
    }
}
