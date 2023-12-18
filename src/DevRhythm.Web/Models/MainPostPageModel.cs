using DevRhythm.App.DTOs;
using DevRhythm.Shared.Entities;
using DevRhythm.Shared.Settings;

namespace DevRhythm.Web.Models
{
    public class MainPostPageModel
    {
        public SortSettings? SortSettings { get; set; }
        public ICollection<TagShortDto> Tags { get; set; } = [];
    }
}
