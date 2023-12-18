namespace DevRhythm.App.DTOs
{
    public class CommentDto
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public string? Content { get; set; }
        public UserShortDto? Author { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ICollection<ReplyDto> Replies { get; set; } = new List<ReplyDto>();
    }
}
