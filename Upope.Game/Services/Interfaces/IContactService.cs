using System.Threading.Tasks;

namespace Upope.Game.Services.Interfaces
{
    public interface IContactService
    {
        Task SyncContactTable(string accessToken, string userId, string contactUserId);
    }
}
