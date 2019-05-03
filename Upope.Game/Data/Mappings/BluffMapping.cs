using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upope.Game.Data.Entities;

namespace Upope.Game.Data.Mappings
{
    public class BluffMapping : IEntityTypeConfiguration<Bluff>
    {
        public void Configure(EntityTypeBuilder<Bluff> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.User)
                .WithMany(x => x.Bluffs)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.UserId);
            builder.HasOne(x => x.GameRound)
                .WithOne(x => x.Bluff)
                .HasForeignKey<Bluff>(x => x.GameRoundId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
