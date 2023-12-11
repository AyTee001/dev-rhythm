﻿namespace DevRhythm.Core.Entities
{
    public class UserAchievement
    {
        public long UserId { get; set; }
        public long AchievementId { get; set;}
        public DateTime EarnedAt { get; set; }
        public Achievement? Achievement { get; set;}
    }
}
