using Microsoft.EntityFrameworkCore;
using Upope.Challange.Data.Entities;
using Upope.Challange.Data.Mappings;

namespace Upope.Challange
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

            //var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            //                     .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();


            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    modelBuilder.ApplyConfiguration(configurationInstance);
            //}

            modelBuilder.ApplyConfiguration<User>(new UserMapping());
            modelBuilder.ApplyConfiguration<Challenge>(new ChallengeMapping());
            modelBuilder.ApplyConfiguration<ChallengeRequest>(new ChallengeRequestMapping());
        }
    }
}
