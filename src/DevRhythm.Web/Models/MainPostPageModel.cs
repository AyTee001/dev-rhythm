using DevRhythm.App.DTOs;
using DevRhythm.Shared.Settings;

namespace DevRhythm.Web.Models
{
    public class MainPostPageModel
    {
        public SortSettings? SortSettings { get; set; }
        public IEnumerable<TagFilterDto> Tags { get; set; } = [];
    }
}
