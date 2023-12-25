namespace DevRhythm.Core.Entities.Base
{
    public abstract class Vote
    {
        public long UserId { get; set; }
        public long EntityId { get; set; }
        public bool IsUpvote { get; set; }
        public User? User { get; set; }
    }
}
