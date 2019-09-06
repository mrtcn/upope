using Microsoft.EntityFrameworkCore;
using Upope.Loyalty.Data.Entities;
using Upope.Loyalty.Data.Mappings;

namespace Upope.Loyalty
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

            modelBuilder.ApplyConfiguration<User>(new UserMapping());
            modelBuilder.ApplyConfiguration(new LoyaltyMapping());
        }
    }
}
