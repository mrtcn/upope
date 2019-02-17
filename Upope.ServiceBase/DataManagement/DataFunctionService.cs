using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using EczacibasiHealth.Utilities.Extensions;

namespace Upope.ServiceBase.DataManagement {
    public class DataFunctionService : IDataFunctionService {
        private readonly IObjectContext _objectContext;

        public DataFunctionService(IObjectContext objectContext) {
            _objectContext = objectContext;
        }

        public IEnumerable<TElement> ExecuteStoreQuery<TElement>(string functionName, object parameters = null) {
            if (parameters == null) {
                return _objectContext.ExecuteStoreQuery<TElement>(functionName);
            }

            var valueDictionary = parameters.ToDictionary();
            var keys = valueDictionary.Select(x => "@" + x.Key).ToArray();

            var format = string.Format("{0} {1}", functionName, string.Join(",", keys));
            var objects = valueDictionary.Select(x => new SqlParameter(string.Format("@{0}",
                x.Key), x.Value)).Cast<object>().ToArray();

            return _objectContext.ExecuteStoreQuery<TElement>(format, objects).ToList();
        }

        public IEnumerable<TElement> ExecuteStoreQuery<TElement>(string functionName, params SqlParameter[] parameters) {
            var keys = parameters.Where(x => x != null).Select(x => "@" + x.ParameterName).ToArray();
            var format = string.Format("{0} {1}", functionName, string.Join(",", keys));

            return _objectContext.ExecuteStoreQuery<TElement>(format,
                parameters.Where(x => x != null).Cast<object>().ToArray()).ToList();
        }
    }
}