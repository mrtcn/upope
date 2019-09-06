using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upope.Loyalty.Data.Entities;
using LoyaltyEntity = Upope.Loyalty.Data.Entities.Loyalty;

namespace Upope.Loyalty.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Loyalty)
                .WithOne(x => x.User)
                .HasPrincipalKey<LoyaltyEntity>(x => x.UserId)
                .HasForeignKey<User>(x => x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
