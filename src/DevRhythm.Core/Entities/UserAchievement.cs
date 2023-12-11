namespace DevRhythm.Core.Entities
{
    public class UserAchievement
    {
        public long UserId { get; set; }
        public long AchievementId { get; set;}
        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;
        public Achievement? Achievement { get; set;}
    }
}
