using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upope.Challenge.Data.Entities;

namespace Upope.Challenge.Data.Mappings
{
    public class ChallengeRequestMapping : IEntityTypeConfiguration<ChallengeRequest>
    {
        public void Configure(EntityTypeBuilder<ChallengeRequest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Challenge)
                .WithMany(x => x.ChallengeRequests)
                .HasForeignKey(x => x.ChallengeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(x => x.Challenger)
                .WithMany(x => x.ReceivedChallengeRequests)
                .HasForeignKey(x => x.ChallengerId)
                .HasPrincipalKey(x => x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(x => x.ChallengOwner)
                .WithMany(x => x.OwnedChallengeRequests)
                .HasForeignKey(x => x.ChallengeOwnerId)
                .HasPrincipalKey(x => x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
