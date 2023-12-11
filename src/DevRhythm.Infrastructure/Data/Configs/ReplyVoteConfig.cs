using DevRhythm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevRhythm.Infrastructure.Data.Configs
{
    public class ReplyVoteConfig : IEntityTypeConfiguration<ReplyVote>
    {
        public void Configure(EntityTypeBuilder<ReplyVote> builder)
        {
            builder.HasKey(e => new { e.UserId, e.ReplyId });

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Reply)
                .WithMany()
                .HasForeignKey(e => e.ReplyId);
        }
    }
}
