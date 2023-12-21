﻿using DevRhythm.Core.Entities.Base;

namespace DevRhythm.Core.Entities
{
    public class DevRhythmUser(string firstName, string lastName, string about, string imageUrl) : Entity 
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string About { get; set; } = about;
        public DateTime? BirthDate { get; set; }
        public string ImageUrl { get; set; } = imageUrl;
        public int Reputation { get; set; }
        public int PostCount { get; set; }
        public int ThreadCount { get; set; }
        public DateTime RegisteredAt { get; set; }

        public DevRhythmIdentityUser? IdentityUser { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Reply> Replies { get; set; } = new List<Reply>();
        public ICollection<UserAchievement> Achievements { get; set; } = new List<UserAchievement>();

    }
}