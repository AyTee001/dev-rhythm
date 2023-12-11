using DevRhythm.Core.Entities.Base;

namespace DevRhythm.Core.Entities
{
    public class PostVote : Vote
    {
        public long PostId { get; set; }
        public Post? Post { get; set; }

    }
}
