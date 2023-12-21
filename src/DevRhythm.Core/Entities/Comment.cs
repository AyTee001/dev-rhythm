using DevRhythm.Core.Entities.Base;

namespace DevRhythm.Core.Entities
{
    public class Comment (string content) : AuditableEntity
    {
        public long PostId { get; set; }
        public string Content { get; set; } = content;
        public DevRhythmUser? Author { get; set; }
        public ICollection<Reply> Replies { get; set; } =  new List<Reply>();
    }
}
