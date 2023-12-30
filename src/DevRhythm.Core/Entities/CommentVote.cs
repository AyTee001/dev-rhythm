using DevRhythm.Core.Entities.Base;

namespace DevRhythm.Core.Entities
{
    public class CommentVote : Vote
    {
        public Comment? Comment { get; set; }
    }
}
