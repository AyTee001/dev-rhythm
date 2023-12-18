namespace DevRhythm.App.DTOs
{
    public class UserShortDto
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageUrl { get; set; }
        public int Reputation { get; set; }
    }
}
