﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Upope.Challange;

namespace Upope.Challange.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190219152043_ChallengeUtils2")]
    partial class ChallengeUtils2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Upope.Challange.Data.Entities.Challenge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChallengeOwnerId");

                    b.Property<string>("ChallengerId");

                    b.Property<int>("RewardPoint");

                    b.Property<int>("Status");

                    b.Property<string>("WinnerId");

                    b.HasKey("Id");

                    b.ToTable("Challenge");
                });

            modelBuilder.Entity("Upope.Challange.Data.Entities.ChallengeRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChallengeId");

                    b.Property<string>("ChallengeOwnerId");

                    b.Property<int>("ChallengeRequestStatus");

                    b.Property<string>("ChallengerId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.ToTable("ChallengeRequest");
                });

            modelBuilder.Entity("Upope.Challange.Data.Entities.ChallengeRequest", b =>
                {
                    b.HasOne("Upope.Challange.Data.Entities.Challenge", "Challenge")
                        .WithMany("ChallengeRequests")
                        .HasForeignKey("ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
