using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upope.Challange.Data.Entities;

namespace Upope.Challange.Data.Mappings
{
    public class ChallangeMapping: IEntityTypeConfiguration<Challenge>
    {
        public void Configure(EntityTypeBuilder<Challenge> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
