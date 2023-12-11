using DevRhythm.Core.Entities.Base;

namespace DevRhythm.Core.Entities
{
    public class Achievement (string title, string description, string iconUrl) : Entity
    {
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public string IconUrl { get; set; } = iconUrl;
    }
}
