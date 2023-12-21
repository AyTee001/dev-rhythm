namespace DevRhythm.Core.Entities.Base
{
    public abstract class Vote
    {
        public long UserId { get; set; }
        public bool IsUpvote { get; set; }
        public DevRhythmUser? User { get; set; }
    }
}
