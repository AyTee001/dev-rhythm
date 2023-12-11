using DevRhythm.Core.Entities.Base;

namespace DevRhythm.Core.Entities
{
    public class CommentVote : Vote
    {
        public long CommentId { get; set; }
        public Comment? Comment { get; set; }
    }
}
