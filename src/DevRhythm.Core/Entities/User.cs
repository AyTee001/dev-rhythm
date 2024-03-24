using DevRhythm.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace DevRhythm.Core.Entities
{
    public class User : IdentityUser<long> 
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        public NotificationCleaningPeriod NotificationCleaningPeriod { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Reply> Replies { get; set; } = new List<Reply>();
    }
}
