using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Upope.ServiceBase.DataManagement {
    public class ContextAdaptor : IObjectSetFactory, IObjectContext {
        private readonly ObjectContext _context;
        private bool _disposed;

        public ContextAdaptor(IObjectContextAdapter context) {
            _context = context.ObjectContext;
        }

        public void ChangeObjectState(object entity, EntityState state) {
            _context.ObjectStateManager.ChangeObjectState(entity, state);
        }

        public ObjectContextOptions ContextOptions {
            get { return _context.ContextOptions; }
        }

        public IObjectSet<T> CreateObjectSet<T>() where T : class {
            return _context.CreateObjectSet<T>();
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing && _context != null) {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public ObjectResult<TElement> ExecuteStoreQuery<TElement>(string functionName, params object[] parameters) {
            return _context.ExecuteStoreQuery<TElement>(functionName, parameters);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync() {
            await _context.SaveChangesAsync();
        }
    }
}