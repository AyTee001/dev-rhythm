using DevRhythm.Core.Entities.Base;

namespace DevRhythm.Core.Entities
{
    public class ReplyVote : Vote
    {
        public Reply? Reply { get; set; }
    }
}
