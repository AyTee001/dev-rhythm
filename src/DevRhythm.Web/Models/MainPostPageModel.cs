using DevRhythm.App.DTOs;

namespace DevRhythm.Web.Models
{
    public class MainPostPageModel
    {
        public IEnumerable<TagFilterDto> Tags { get; set; } = [];
    }
}
