using DevRhythm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevRhythm.Infrastructure.Data.Configs
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasMany(e => e.Replies)
                .WithOne()
                .HasForeignKey(e => e.CommentId);
        }
    }
}
