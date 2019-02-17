using System;
using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;

namespace Upope.ServiceBase.DataManagement {
    public interface IObjectContext : IDisposable {
        ObjectContextOptions ContextOptions { get; }
        void SaveChanges();
        Task SaveChangesAsync();
        ObjectResult<TElement> ExecuteStoreQuery<TElement>(string functionName, params object[] parameters);
    }
}