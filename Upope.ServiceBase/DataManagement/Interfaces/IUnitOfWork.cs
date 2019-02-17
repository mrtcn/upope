using System;
using System.Threading.Tasks;

namespace Upope.ServiceBase.DataManagement {
    public interface IUnitOfWork : IDisposable {
        bool LazyLoadingEnabled { set; get; }
        void Commit();
        Task CommitAsync();
    }
}