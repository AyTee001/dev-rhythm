using DevRhythm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevRhythm.Infrastructure.Data.Configs
{
    public class UserAchievementConfig : IEntityTypeConfiguration<UserAchievement>
    {
        public void Configure(EntityTypeBuilder<UserAchievement> builder)
        {
            builder.HasKey(e => new { e.UserId, e.AchievementId });

            builder.HasOne(e => e.Achievement)
                .WithMany()
                .HasForeignKey(e => e.AchievementId);
        }
    }
}
