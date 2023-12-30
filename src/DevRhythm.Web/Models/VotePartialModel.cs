using DevRhythm.Shared.Enums;

namespace DevRhythm.Web.Models
{
    public class VotePartialModel
    {
        public Guid Guid { get; set; }
        public VoteType VoteType { get; set; }
        public int VoteResult { get; set; }
        public long EntityId { get; set; }
        public long? UserId { get; set; }
        public bool IsUpvoted { get; set; }
        public bool IsDownvoted { get; set; }
    }
}
