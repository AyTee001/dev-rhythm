using DevRhythm.App.DTOs;
using DevRhythm.Shared.Settings;

namespace DevRhythm.Web.Models
{
    public class MainPostPageModel
    {
        public ICollection<PostShortDto> PostPreviewsCollection { get; set; } = new List<PostShortDto>();
        public SortSettings? SortSettings { get; set; }
    }
}
