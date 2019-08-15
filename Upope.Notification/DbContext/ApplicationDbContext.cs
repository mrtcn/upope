using Microsoft.EntityFrameworkCore;
using Upope.Notification.Data.Mappings;

namespace Upope.Notification
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new NotificationMapping());
            modelBuilder.ApplyConfiguration(new NotificationTemplateMapping());
        }
    }
}
