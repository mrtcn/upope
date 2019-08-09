using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upope.Chat.Data.Entities;

namespace Upope.Chat.Data.Mappings
{
    public class ContactMapping : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
        }
    }
}
