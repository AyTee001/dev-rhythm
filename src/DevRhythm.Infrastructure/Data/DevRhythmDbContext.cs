using DevRhythm.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevRhythm.Infrastructure.Data
{
    public class DevRhythmDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Achievement> Achievements => Set<Achievement>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<CommentVote> CommentsVotes => Set<CommentVote>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<PostTag> PostTags => Set<PostTag>();
        public DbSet<PostVote> PostVotes => Set<PostVote>();
        public DbSet<Reply> Replies => Set<Reply>();
        public DbSet<ReplyVote> RepliesVotes => Set<ReplyVote>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<DevRhythmUser> DevRhythmUsers => Set<DevRhythmUser>();
        public DbSet<UserAchievement> UserAchievements => Set<UserAchievement>();
        public DbSet<UserNotification> UserNotifications => Set<UserNotification>();

        public DevRhythmDbContext(DbContextOptions<DevRhythmDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configure();
            modelBuilder.Seed();
        }
    }
}
