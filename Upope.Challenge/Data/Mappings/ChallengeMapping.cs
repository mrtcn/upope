using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChallengeEntity = Upope.Challenge.Data.Entities.Challenge;

namespace Upope.Challenge.Data.Mappings
{
    public class ChallengeMapping: IEntityTypeConfiguration<ChallengeEntity>
    {
        public void Configure(EntityTypeBuilder<ChallengeEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
