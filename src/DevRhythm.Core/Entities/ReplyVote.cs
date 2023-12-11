using DevRhythm.Core.Entities.Base;

namespace DevRhythm.Core.Entities
{
    public class ReplyVote : Vote
    {
        public long ReplyId { get; set; }
        public Reply? Reply { get; set; }
    }
}
