namespace DevRhythm.App.DTOs
{
    public class ReplyDto
    {
        public long Id { get; set; }
        public long CommentId { get; set; }
        public string? Content { get; set; }
        public UserShortDto? Author { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
