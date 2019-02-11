using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Upope.Identity.DbContext;

namespace Upope.Identity
{
    public class ApplicationUserDbContextFactory : IDesignTimeDbContextFactory<ApplicationUserDbContext>
    {
        public ApplicationUserDbContext CreateDbContext(string[] args)
        {
            var dbContext = new ApplicationUserDbContext(new DbContextOptionsBuilder<ApplicationUserDbContext>().UseSqlServer(
               new ConfigurationBuilder()
                   .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.json"))
                   .Build()
                   .GetConnectionString("DatabaseConnection")
               ).Options);

            dbContext.Database.Migrate();
            return dbContext;
        }
    }
}
