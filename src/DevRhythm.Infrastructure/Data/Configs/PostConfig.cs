using DevRhythm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevRhythm.Infrastructure.Data.Configs
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasMany(e => e.Comments)
                .WithOne()
                .HasForeignKey(e => e.PostId);

            builder.HasMany(e => e.Tags)
                .WithMany(e => e.Posts)
                .UsingEntity<PostTag>();
        }
    }
}
