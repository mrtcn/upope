using Microsoft.EntityFrameworkCore;
using Upope.Chat.Data.Mappings;

namespace Upope.Chat
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

            modelBuilder.ApplyConfiguration(new ChatRoomMapping());
            modelBuilder.ApplyConfiguration(new ChatMapping());
            modelBuilder.ApplyConfiguration(new ContactMapping());

            //    modelBuilder.Entity<Point>().HasData(
            //        new Point() { Id = 1, Credit = 10, Status = Status.Active, UserId = "0159adeb-1a12-4edb-8052-c860125ee58b" },
            //        new Point() { Id = 2, Credit = 20, Status = Status.Active, UserId = "6a1cb14d-49e0-498c-8a10-4a08f1e059f0" },
            //        new Point() { Id = 3, Credit = 30, Status = Status.Active, UserId = "7a41ec8c-c42f-46ef-a0e9-e840f058df5b" },
            //        new Point() { Id = 4, Credit = 40, Status = Status.Active, UserId = "50d7fc43-e90b-48c8-9d07-455dab9c8753" },
            //        new Point() { Id = 5, Credit = 50, Status = Status.Active, UserId = "ceb3bc69-f4b8-4407-955e-76ffde854ad8" },
            //        new Point() { Id = 6, Credit = 60, Status = Status.Active, UserId = "c37a7280-1190-4f87-ac96-5f3b71cdbd2b" },
            //        new Point() { Id = 7, Credit = 70, Status = Status.Active, UserId = "6c0093b8-4d59-40be-b75b-00f65015f9cb" },
            //        new Point() { Id = 8, Credit = 80, Status = Status.Active, UserId = "ec0e747a-bd66-47e5-9c2f-6b9eb187cee4" },
            //        new Point() { Id = 9, Credit = 90, Status = Status.Active, UserId = "09c2d09d-7961-4ba2-8959-c6df64b6fad9" },
            //        new Point() { Id = 10, Credit = 100, Status = Status.Active, UserId = "3e30b604-7d9c-4a3b-a483-7b74c62756d6" });
            //
        }
    }
}
