namespace DevRhythm.App.DTOs
{
    public class PostShortDto
    {
        public long Id { get; set; }
        public string? Heading { get; set; }
        public int VoteResult { get; set; }
        public UserShortDto? Author { get; set; }
        public string? PreviewContent { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ICollection<TagShortDto> Tags { get; set; } = new List<TagShortDto>();
    }
}
