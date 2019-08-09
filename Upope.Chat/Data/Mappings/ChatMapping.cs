using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatEntity = Upope.Chat.Data.Entities.Chat;

namespace Upope.Chat.Data.Mappings
{
    public class ChatMapping : IEntityTypeConfiguration<ChatEntity>
    {
        public void Configure(EntityTypeBuilder<ChatEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.ChatRoom)
                .WithMany(x => x.Chats)
                .HasForeignKey(x => x.ChatRoomId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
