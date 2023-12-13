using DevRhythm.Core.Entities.Base;
using DevRhythm.Core.Entities;

namespace DevRhythm.App.DTOs
{
    public class PostFullDto : Entity
    {
            public string? Heading { get; set; }
            public string? Content { get; set; }
            public int VoteResult { get; set; }
            public UserShortDto? Author { get; set; }
            public ICollection<Comment> Comments { get; set; } = new List<Comment>();
            public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
