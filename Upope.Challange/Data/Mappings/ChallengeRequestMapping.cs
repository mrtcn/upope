﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upope.Challange.Data.Entities;

namespace Upope.Challange.Data.Mappings
{
    public class ChallengeRequestMapping : IEntityTypeConfiguration<ChallengeRequest>
    {
        public void Configure(EntityTypeBuilder<ChallengeRequest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Challenge)
                .WithMany(x => x.ChallengeRequests)
                .HasForeignKey(x => x.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Challenger)
                .WithMany(x => x.ChallengeRequests)
                .HasForeignKey(x => x.ChallengerId)
                .HasPrincipalKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
