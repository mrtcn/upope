using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upope.Chat.Data.Entities;

namespace Upope.Chat.Data.Mappings
{
    public class ChatRoomMapping : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
        }
    }
}
