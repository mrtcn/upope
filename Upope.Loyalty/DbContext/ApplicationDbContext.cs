using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Upope.Loyalty.Data.Entities;
using Upope.Loyalty.Data.Mappings;
using Upope.ServiceBase.Enums;

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

            modelBuilder.ApplyConfiguration<Point>(new PointMapping());

            modelBuilder.Entity<Point>().HasData(
                new Point() { Id = 1, Points = 10, Status = Status.Active, UserId = "0fc04ca0-cc8d-4896-a50d-b9fe247cf152" },
                new Point() { Id = 2, Points = 20, Status = Status.Active, UserId = "3282d1a0-ed0e-4f8a-8720-8f3474c21ecf" },
                new Point() { Id = 3, Points = 30, Status = Status.Active, UserId = "ad687285-5202-4f81-8220-0a424dae3566" },
                new Point() { Id = 4, Points = 40, Status = Status.Active, UserId = "897b2d91-6c1a-4bed-a4fc-9e84441e4b2d" },
                new Point() { Id = 5, Points = 50, Status = Status.Active, UserId = "5961efe4-8ad8-488f-8727-022417b612a0" },
                new Point() { Id = 6, Points = 60, Status = Status.Active, UserId = "d589a0cf-7172-4b44-bb49-609fcc7d56be" },
                new Point() { Id = 7, Points = 70, Status = Status.Active, UserId = "e21b42d9-ca1f-49d5-b2f8-565a1bff3754" },
                new Point() { Id = 8, Points = 80, Status = Status.Active, UserId = "b41b9a6b-00da-46dc-8115-67066009b211" },
                new Point() { Id = 9, Points = 90, Status = Status.Active, UserId = "3eec55e4-3f38-4191-b43f-ba950108b296" },
                new Point() { Id = 10, Points = 100, Status = Status.Active, UserId = "04dc0f14-65ce-4d93-8505-124d9f3b9276" });
        }
    }
}
