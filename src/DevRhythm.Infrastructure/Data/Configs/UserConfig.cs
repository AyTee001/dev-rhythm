using DevRhythm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevRhythm.Infrastructure.Data.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
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
        }
    }
}
