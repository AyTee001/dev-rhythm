using DevRhythm.Shared.Settings;

namespace DevRhythm.Web.DTOs
{
    public class PageGetDto
    {
        public int PageNumber { get; set; }
        public int PostCount { get; set; }
        public ICollection<long> TagIds { get; set; } = [];
        public SortSettings? SortSettings { get; set; }
        public string Keyword { get; set; } = string.Empty;
    }
}
