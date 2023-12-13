using DevRhythm.Shared.Enums;

namespace DevRhythm.Shared.Settings
{
    public record SortSettings()
    {
        public SortProperty SortProperty { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
