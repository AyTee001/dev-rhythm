using DevRhythm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevRhythm.Infrastructure.Data.Configs
{
    public class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(e => e.Sender)
                .WithMany()
                .HasForeignKey(e => e.SenderId);

            builder.HasOne(e => e.Post)
                .WithMany()
                .HasForeignKey(e => e.PostId);
        }
    }
}
