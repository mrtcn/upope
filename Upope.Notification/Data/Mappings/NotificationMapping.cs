using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upope.Notification.Data.Entities;
using NotificationEntity = Upope.Notification.Data.Entities.Notification;
namespace Upope.Notification.Data.Mappings
{
    public class NotificationMapping : IEntityTypeConfiguration<NotificationEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
        }
    }

    public class NotificationCultureMapping : IEntityTypeConfiguration<NotificationCulture>
    {
        public void Configure(EntityTypeBuilder<NotificationCulture> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.BaseEntity).WithMany(x => x.CulturedEntities).HasForeignKey(x => x.BaseEntityId);
        }
    }
}
