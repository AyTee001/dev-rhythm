using DevRhythm.Core.Entities.Base;

namespace DevRhythm.Core.Entities
{
    public class Reply (string content) : AuditableEntity
    {
        public long CommentId { get; set; }
        public string Content { get; set; } = content;
        public DevRhythmUser? Author { get; set; }
    }
}
