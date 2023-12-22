using DevRhythm.Core.Entities;
using DevRhythm.Core.Entities.Base;
using DevRhythm.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevRhythm.Infrastructure.Data
{
    public class DevRhythmDbContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        private readonly IUserInfoProvider _userInfoProvider;

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
        public DbSet<UserAchievement> UserAchievements => Set<UserAchievement>();
        public DbSet<UserNotification> UserNotifications => Set<UserNotification>();

        public DevRhythmDbContext(DbContextOptions<DevRhythmDbContext> options, IUserInfoProvider userInfoProvider) : base(options) 
        { 
            _userInfoProvider = userInfoProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configure();
            modelBuilder.Seed();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditEntityData();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditEntityData()
        {
            ChangeTracker.DetectChanges();
            var added = ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Added && t.Entity is AuditableEntity)
                        .ToArray();

            foreach (var entity in added)
            {
                var auditEntity = entity.Entity as AuditableEntity;

                if (auditEntity is not null && Users.Any(e => e.Id == _userInfoProvider.Id))
                {
                    auditEntity.CreatedAt = DateTime.UtcNow;
                    auditEntity.CreatedBy = (long)_userInfoProvider.Id!;
                }
            }

        }
    }
}
