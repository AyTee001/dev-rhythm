using DevRhythm.Shared.Enums;

namespace DevRhythm.Shared.DTOs
{
    public class NewVoteDto
    {
        public VoteType VoteType { get; set; }
        public long EntityId { get; set; }
        public long UserId { get; set; }
        public bool IsUpvote { get; set; }
    }
}
