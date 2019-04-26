using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameEntity = Upope.Game.Data.Entities.Game;

namespace Upope.Game.Data.Mappings
{
    public class GameMapping: IEntityTypeConfiguration<GameEntity>
    {
        public void Configure(EntityTypeBuilder<GameEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.HostUser)
                .WithMany(x => x.HostGames)
                .HasForeignKey(x => x.HostUserId)
                .HasPrincipalKey(x => x.UserId);
            builder.HasOne(x => x.GuestUser)
                .WithMany(x => x.GuestGames)
                .HasForeignKey(x => x.GuestUserId)
                .HasPrincipalKey(x => x.UserId);
        }
    }
}
