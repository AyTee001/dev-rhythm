using DevRhythm.Core.Entities.Base;

namespace DevRhythm.Core.Entities
{
    public class Post (string heading, string content) : AuditableEntity
    {
        public string Heading { get; set; } = heading;
        public string Content { get; set; } = content;
        public int VoteResult { get; set; }
        public User? Author { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
