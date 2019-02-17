using Microsoft.EntityFrameworkCore;
using System;

namespace Upope.ServiceBase.DataManagement {
    public interface IObjectSetFactory : IDisposable {
        IObjectSet<T> CreateObjectSet<T>() where T : class;
        void ChangeObjectState(object entity, EntityState state);
    }
}