﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Upope.Identity.DbContext;

namespace Upope.Identity.Migrations
{
    [DbContext(typeof(ApplicationUserDbContext))]
    [Migration("20190217133017_BirthdayColumn2")]
    partial class BirthdayColumn2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Upope.Identity.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FacebookId")
                        .HasMaxLength(250);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("Gender");

                    b.Property<string>("GoogleId")
                        .HasMaxLength(250);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PictureUrl");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "dfacb498-e2cf-4195-bf1d-035918675e8c",
                            AccessFailedCount = 0,
                            Birthday = new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "587b9c25-b1c3-4816-9dc2-f6d883280cf3",
                            Email = "muratcantuna1@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Murat",
                            Gender = 1,
                            LastName = "Tuna",
                            LockoutEnabled = true,
                            NormalizedEmail = "MURATCANTUNA1@GMAIL.COM",
                            NormalizedUserName = "MURATCANTUNA1",
                            PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
                            TwoFactorEnabled = false,
                            UserName = "muratcantuna1"
                        },
                        new
                        {
                            Id = "99f8f578-b00f-483e-aed4-d34a92dd665d",
                            AccessFailedCount = 0,
                            Birthday = new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "004689a0-352a-45ea-abf4-85ffc9b8daf1",
                            Email = "muratcantuna2@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Murat",
                            Gender = 1,
                            LastName = "Tuna",
                            LockoutEnabled = true,
                            NormalizedEmail = "MURATCANTUNA2@GMAIL.COM",
                            NormalizedUserName = "MURATCANTUNA2",
                            PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
                            TwoFactorEnabled = false,
                            UserName = "muratcantuna2"
                        },
                        new
                        {
                            Id = "141c644a-7aee-431a-b2ef-96809189373c",
                            AccessFailedCount = 0,
                            Birthday = new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "0a19aa2a-c25a-427f-b544-72f4cf8c63ac",
                            Email = "muratcantuna3@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Murat",
                            Gender = 1,
                            LastName = "Tuna",
                            LockoutEnabled = true,
                            NormalizedEmail = "MURATCANTUNA3@GMAIL.COM",
                            NormalizedUserName = "MURATCANTUNA3",
                            PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
                            TwoFactorEnabled = false,
                            UserName = "muratcantuna3"
                        },
                        new
                        {
                            Id = "94731436-cd76-4845-808f-041db4965ebc",
                            AccessFailedCount = 0,
                            Birthday = new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "423adb42-5449-4c15-b798-7c8d64dac83f",
                            Email = "muratcantuna4@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Murat",
                            Gender = 1,
                            LastName = "Tuna",
                            LockoutEnabled = true,
                            NormalizedEmail = "MURATCANTUNA4@GMAIL.COM",
                            NormalizedUserName = "MURATCANTUNA4",
                            PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
                            TwoFactorEnabled = false,
                            UserName = "muratcantuna4"
                        },
                        new
                        {
                            Id = "d5fe2d42-f323-4742-ad6e-069d251223af",
                            AccessFailedCount = 0,
                            Birthday = new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "177933fb-eb9b-4310-a679-2c20bf3ee704",
                            Email = "muratcantuna5@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Murat",
                            Gender = 1,
                            LastName = "Tuna",
                            LockoutEnabled = true,
                            NormalizedEmail = "MURATCANTUNA5@GMAIL.COM",
                            NormalizedUserName = "MURATCANTUNA5",
                            PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
                            TwoFactorEnabled = false,
                            UserName = "muratcantuna5"
                        },
                        new
                        {
                            Id = "91eb6968-19ac-48e0-afbc-29e80139a955",
                            AccessFailedCount = 0,
                            Birthday = new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "029142f3-90c1-4f39-9c5c-e77efe5c6e3d",
                            Email = "muratcantuna6@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Murat",
                            Gender = 1,
                            LastName = "Tuna",
                            LockoutEnabled = true,
                            NormalizedEmail = "MURATCANTUNA6@GMAIL.COM",
                            NormalizedUserName = "MURATCANTUNA6",
                            PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
                            TwoFactorEnabled = false,
                            UserName = "muratcantuna6"
                        },
                        new
                        {
                            Id = "fa222a86-ef7c-4bde-ab3d-ad61901dadbd",
                            AccessFailedCount = 0,
                            Birthday = new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "1754ff59-8d68-4ec2-b577-633fafffb9a3",
                            Email = "muratcantuna7@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Murat",
                            Gender = 1,
                            LastName = "Tuna",
                            LockoutEnabled = true,
                            NormalizedEmail = "MURATCANTUNA7@GMAIL.COM",
                            NormalizedUserName = "MURATCANTUNA7",
                            PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
                            TwoFactorEnabled = false,
                            UserName = "muratcantuna7"
                        },
                        new
                        {
                            Id = "a2582b18-e7b8-4d45-b514-78f670d85aa9",
                            AccessFailedCount = 0,
                            Birthday = new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "4bf4094b-d5b7-43a2-b809-557868a2866f",
                            Email = "muratcantuna8@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Murat",
                            Gender = 1,
                            LastName = "Tuna",
                            LockoutEnabled = true,
                            NormalizedEmail = "MURATCANTUNA8@GMAIL.COM",
                            NormalizedUserName = "MURATCANTUNA8",
                            PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
                            TwoFactorEnabled = false,
                            UserName = "muratcantuna8"
                        },
                        new
                        {
                            Id = "4790b8d3-a478-4794-932f-0045ddcdba5c",
                            AccessFailedCount = 0,
                            Birthday = new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "662c5e34-9c02-4cf2-85d1-8efaf3ead5b4",
                            Email = "muratcantuna9@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Murat",
                            Gender = 1,
                            LastName = "Tuna",
                            LockoutEnabled = true,
                            NormalizedEmail = "MURATCANTUNA9@GMAIL.COM",
                            NormalizedUserName = "MURATCANTUNA9",
                            PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
                            TwoFactorEnabled = false,
                            UserName = "muratcantuna9"
                        },
                        new
                        {
                            Id = "692603a9-5181-4021-ae94-1c1fb80b0ae4",
                            AccessFailedCount = 0,
                            Birthday = new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "5c228b08-fad2-4483-a266-1581217d9cf7",
                            Email = "muratcantuna10@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Murat",
                            Gender = 1,
                            LastName = "Tuna",
                            LockoutEnabled = true,
                            NormalizedEmail = "MURATCANTUNA10@GMAIL.COM",
                            NormalizedUserName = "MURATCANTUNA10",
                            PasswordHash = "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD",
                            TwoFactorEnabled = false,
                            UserName = "muratcantuna10"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Upope.Identity.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Upope.Identity.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Upope.Identity.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Upope.Identity.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
