using AutoMapper;
using System.Linq;
using Upope.Chat.Data.Entities;
using Upope.Chat.EntityParams;
using Upope.Chat.Services.Interfaces;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;

namespace Upope.Chat.Services
{    
    public class ContactService : EntityServiceBase<Contact>, IContactService
    {
        private readonly IMapper _mapper;
        public ContactService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
        }

        public bool IsInContact(string userId, string contactUserId)
        {
            var contact = GetContact(userId, contactUserId);

            return contact != null;
        }

        public ContactParams GetContact(string userId, string contactUserId)
        {
            var contact = Entities.FirstOrDefault(x =>
            ((x.UserId == userId && x.ContactUserId == contactUserId)
            || (x.UserId == contactUserId && x.ContactUserId == userId))
            && x.Status == Status.Active);

            if (contact == null)
                return null;

            var contactParams = _mapper.Map<ContactParams>(contact);
            return contactParams;
        }
    }
}
