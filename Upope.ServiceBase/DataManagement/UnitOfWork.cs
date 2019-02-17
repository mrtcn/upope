using System;
using System.Threading.Tasks;

namespace Upope.ServiceBase.DataManagement {
    public class UnitOfWork : IUnitOfWork {
        private readonly IObjectContext _objectContext;
        private bool _disposed;

        public UnitOfWork(IObjectContext objectContext) {
            _objectContext = objectContext;
        }

        public bool LazyLoadingEnabled {
            set { _objectContext.ContextOptions.LazyLoadingEnabled = value; }
            get { return _objectContext.ContextOptions.LazyLoadingEnabled; }
        }

        public void Commit() {
            _objectContext.SaveChanges();
        }

        public async Task CommitAsync() {
            await _objectContext.SaveChangesAsync();
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing && _objectContext != null) {
                    _objectContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}