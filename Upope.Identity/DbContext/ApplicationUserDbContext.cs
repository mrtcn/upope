using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Upope.Identity.Entities;

namespace Upope.Identity.DbContext
{
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<ApplicationUser>().HasData(
            //    new ApplicationUser()
            //    {
            //        Email = "muratcantuna1@gmail.com",
            //        FirstName = "Murat",
            //        LastName = "Tuna",
            //        Gender = Enum.Gender.Male,
            //        EmailConfirmed = true,
            //        NormalizedEmail = "MURATCANTUNA1@GMAIL.COM",
            //        NormalizedUserName = "MURATCANTUNA1",
            //        UserName = "muratcantuna1",
            //        PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
            //        SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
            //        LockoutEnabled = true,
            //        Birthday=new System.DateTime(1984, 12, 16, 12, 23, 26)
            //    },
            //new ApplicationUser()
            //{
            //    Email = "muratcantuna2@gmail.com",
            //    FirstName = "Murat",
            //    LastName = "Tuna",
            //    Gender = Enum.Gender.Male,
            //    EmailConfirmed = true,
            //    NormalizedEmail = "MURATCANTUNA2@GMAIL.COM",
            //    NormalizedUserName = "MURATCANTUNA2",
            //    UserName = "muratcantuna2",
            //    PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
            //    SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
            //    LockoutEnabled = true,
            //    Birthday = new System.DateTime(1984, 12, 16, 12, 23, 26)
            //},
            //new ApplicationUser()
            //{
            //    Email = "muratcantuna3@gmail.com",
            //    FirstName = "Murat",
            //    LastName = "Tuna",
            //    Gender = Enum.Gender.Male,
            //    EmailConfirmed = true,
            //    NormalizedEmail = "MURATCANTUNA3@GMAIL.COM",
            //    NormalizedUserName = "MURATCANTUNA3",
            //    UserName = "muratcantuna3",
            //    PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
            //    SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
            //    LockoutEnabled = true,
            //    Birthday = new System.DateTime(1984, 12, 16, 12, 23, 26)
            //},
            //new ApplicationUser()
            //{
            //    Email = "muratcantuna4@gmail.com",
            //    FirstName = "Murat",
            //    LastName = "Tuna",
            //    Gender = Enum.Gender.Male,
            //    EmailConfirmed = true,
            //    NormalizedEmail = "MURATCANTUNA4@GMAIL.COM",
            //    NormalizedUserName = "MURATCANTUNA4",
            //    UserName = "muratcantuna4",
            //    PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
            //    SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
            //    LockoutEnabled = true,
            //    Birthday = new System.DateTime(1984, 12, 16, 12, 23, 26)
            //},
            //new ApplicationUser()
            //{
            //    Email = "muratcantuna5@gmail.com",
            //    FirstName = "Murat",
            //    LastName = "Tuna",
            //    Gender = Enum.Gender.Male,
            //    EmailConfirmed = true,
            //    NormalizedEmail = "MURATCANTUNA5@GMAIL.COM",
            //    NormalizedUserName = "MURATCANTUNA5",
            //    UserName = "muratcantuna5",
            //    PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
            //    SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
            //    LockoutEnabled = true,
            //    Birthday = new System.DateTime(1984, 12, 16, 12, 23, 26)
            //},
            //new ApplicationUser()
            //{
            //    Email = "muratcantuna6@gmail.com",
            //    FirstName = "Murat",
            //    LastName = "Tuna",
            //    Gender = Enum.Gender.Male,
            //    EmailConfirmed = true,
            //    NormalizedEmail = "MURATCANTUNA6@GMAIL.COM",
            //    NormalizedUserName = "MURATCANTUNA6",
            //    UserName = "muratcantuna6",
            //    PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
            //    SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
            //    LockoutEnabled = true,
            //    Birthday = new System.DateTime(1984, 12, 16, 12, 23, 26)
            //},
            //new ApplicationUser()
            //{
            //    Email = "muratcantuna7@gmail.com",
            //    FirstName = "Murat",
            //    LastName = "Tuna",
            //    Gender = Enum.Gender.Male,
            //    EmailConfirmed = true,
            //    NormalizedEmail = "MURATCANTUNA7@GMAIL.COM",
            //    NormalizedUserName = "MURATCANTUNA7",
            //    UserName = "muratcantuna7",
            //    PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
            //    SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
            //    LockoutEnabled = true,
            //    Birthday = new System.DateTime(1984, 12, 16, 12, 23, 26)
            //},
            //new ApplicationUser()
            //{
            //    Email = "muratcantuna8@gmail.com",
            //    FirstName = "Murat",
            //    LastName = "Tuna",
            //    Gender = Enum.Gender.Male,
            //    EmailConfirmed = true,
            //    NormalizedEmail = "MURATCANTUNA8@GMAIL.COM",
            //    NormalizedUserName = "MURATCANTUNA8",
            //    UserName = "muratcantuna8",
            //    PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
            //    SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
            //    LockoutEnabled = true,
            //    Birthday = new System.DateTime(1984, 12, 16, 12, 23, 26)
            //},
            //new ApplicationUser()
            //{
            //    Email = "muratcantuna9@gmail.com",
            //    FirstName = "Murat",
            //    LastName = "Tuna",
            //    Gender = Enum.Gender.Male,
            //    EmailConfirmed = true,
            //    NormalizedEmail = "MURATCANTUNA9@GMAIL.COM",
            //    NormalizedUserName = "MURATCANTUNA9",
            //    UserName = "muratcantuna9",
            //    PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
            //    SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
            //    LockoutEnabled = true,
            //    Birthday = new System.DateTime(1984, 12, 16, 12, 23, 26)
            //},
            //new ApplicationUser()
            //{
            //    Email = "muratcantuna10@gmail.com",
            //    FirstName = "Murat",
            //    LastName = "Tuna",
            //    Gender = Enum.Gender.Male,
            //    EmailConfirmed = true,
            //    NormalizedEmail = "MURATCANTUNA10@GMAIL.COM",
            //    NormalizedUserName = "MURATCANTUNA10",
            //    UserName = "muratcantuna10",
            //    PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
            //    SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
            //    LockoutEnabled = true,
            //    Birthday = new System.DateTime(1984, 12, 16, 12, 23, 26)
            //});
        }
    }
}
