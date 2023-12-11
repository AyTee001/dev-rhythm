using DevRhythm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevRhythm.Infrastructure.Data.Configs
{
    public class PostVoteConfig : IEntityTypeConfiguration<PostVote>
    {
        public void Configure(EntityTypeBuilder<PostVote> builder)
        {
            builder.HasKey(e => new { e.UserId, e.PostId });

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Post)
                .WithMany()
                .HasForeignKey(e => e.PostId);
        }
    }
}
