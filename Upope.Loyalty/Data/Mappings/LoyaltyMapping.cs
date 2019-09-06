using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upope.Loyalty.Data.Entities;
using LoyaltyEntity = Upope.Loyalty.Data.Entities.Loyalty;

namespace Upope.Loyalty.Data.Mappings
{
    public class LoyaltyMapping: IEntityTypeConfiguration<LoyaltyEntity>
    {
        public void Configure(EntityTypeBuilder<LoyaltyEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();            
        }
    }
}
