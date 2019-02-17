using System.Collections.Generic;
using System.Data.SqlClient;

namespace Upope.ServiceBase.DataManagement {
    public interface IDataFunctionService : IDependency {
        IEnumerable<TElement> ExecuteStoreQuery<TElement>(string functionName, object parameters = null);
        IEnumerable<TElement> ExecuteStoreQuery<TElement>(string functionName, params SqlParameter[] parameters);
    }
}