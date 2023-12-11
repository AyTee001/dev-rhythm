using DevRhythm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevRhythm.Infrastructure.Data.Configs
{
    public class UserNotificationConfig : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder.HasKey(e => new { e.ReceiverId, e.NotificationId });

            builder.HasOne(e => e.Notification)
                .WithMany()
                .HasForeignKey(e => e.NotificationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Receiver)
                .WithMany()
                .HasForeignKey(e => e.ReceiverId);

        }
    }
}
