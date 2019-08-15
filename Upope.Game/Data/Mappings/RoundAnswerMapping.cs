using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upope.Game.Data.Entities;

namespace Upope.Game.Data.Mappings
{
    public class RoundAnswerMapping : IEntityTypeConfiguration<RoundAnswer>
    {
        public void Configure(EntityTypeBuilder<RoundAnswer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.GameRound)
                .WithMany(x => x.RoundAnswers)
                .HasForeignKey(x => x.GameRoundId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.RoundAnswers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
