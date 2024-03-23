namespace DevRhythm.App.DTOs
{
    public class PostCreateDto
    {
        public string? Heading { get; set; }
        public string? Content { get; set; }
        public long CreatedBy { get; set; }
        public List<TagFilterDto> Tags { get; set; } = [];

    }
}
