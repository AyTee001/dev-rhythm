using Microsoft.AspNetCore.Identity;

namespace DevRhythm.Core.Entities
{
    public class DevRhythmIdentityUser : IdentityUser
    {
        public long DevRhythmUserId { get; set; }
        public DevRhythmUser? DevRhythmUser { get; set; }
    }
}
