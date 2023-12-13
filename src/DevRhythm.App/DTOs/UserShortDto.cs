using DevRhythm.Core.Entities.Base;

namespace DevRhythm.App.DTOs
{
    public class UserShortDto : Entity
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageUrl { get; set; }
        public int Reputation { get; set; }
    }
}
