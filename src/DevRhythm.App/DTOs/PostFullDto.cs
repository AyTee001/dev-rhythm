namespace DevRhythm.App.DTOs
{
    public class PostFullDto
    {
            public long Id { get; set; }
            public string? Heading { get; set; }
            public string? Content { get; set; }
            public int VoteResult { get; set; }
            public UserShortDto? Author { get; set; }
            public DateTime? CreatedAt { get; set; }
            public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
            public ICollection<TagShortDto> Tags { get; set; } = new List<TagShortDto>();
    }
}
