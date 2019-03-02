using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Upope.Loyalty.Data.Mappings
{
    public class LoyaltyMapping: IEntityTypeConfiguration<Entities.Loyalty>
    {
        public void Configure(EntityTypeBuilder<Entities.Loyalty> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
        }
    }
}
