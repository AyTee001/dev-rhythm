using DevRhythm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevRhythm.Infrastructure.Data.Configs
{
    public class UserConfig : IEntityTypeConfiguration<DevRhythmUser>
    {
        public void Configure(EntityTypeBuilder<DevRhythmUser> builder)
        {
            builder.HasMany(e => e.Replies)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Comments)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Posts)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Achievements)
                .WithOne()
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.IdentityUser)
                .WithOne(e => e.DevRhythmUser)
                .HasForeignKey<DevRhythmIdentityUser>(e => e.DevRhythmUserId);
        }
    }
}
