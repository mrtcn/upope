using Upope.Chat.Data.Entities;
using Upope.Chat.EntityParams;
using Upope.ServiceBase;

namespace Upope.Chat.Services.Interfaces
{
    public interface IContactService : IEntityServiceBase<Contact>
    {
        bool IsInContact(string userId, string contactUserId);
        ContactParams GetContact(string userId, string contactUserId);
    }
}
