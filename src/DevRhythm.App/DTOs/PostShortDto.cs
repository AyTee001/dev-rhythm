using DevRhythm.Core.Entities;
using DevRhythm.Core.Entities.Base;

namespace DevRhythm.App.DTOs
{
    public class PostShortDto : AuditableEntity
    {
        public string? Heading { get; set; }
        public int VoteResult { get; set; }
        public UserShortDto? Author { get; set; }
        public string? PreviewContent { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
