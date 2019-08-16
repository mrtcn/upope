using Upope.ServiceBase.Interfaces;

namespace Upope.ServiceBase.ServiceBase.Models {
    public class RemoveEntityParams : IRemoveEntityParams {
        public int Id { get; set; }

        public RemoveEntityParams() {
        }

        public RemoveEntityParams(int id, IHasOperator hasOperator,
            bool removePermanently = false, bool checkRelationalEntities = true) {
            Id = id;
            OperatorId = hasOperator.OperatorId;
            OperatorType = hasOperator.OperatorType;
            RemovePermanently = removePermanently;
            CheckRelationalEntities = checkRelationalEntities;
        }

        public RemoveEntityParams(int id, IHasOperator hasOperator, string culture,
            bool removePermanently = false, bool checkRelationalEntities = true) {
            Id = id;
            OperatorId = hasOperator.OperatorId;
            OperatorType = hasOperator.OperatorType;
            RemovePermanently = removePermanently;
            CheckRelationalEntities = checkRelationalEntities;
            Culture = culture;
        }

        public bool CheckRelationalEntities { get; set; }

        public string Culture { get; set; }
        public int OperatorId { get; set; }
        public OperatorType OperatorType { get; set; }
        public bool RemovePermanently { get; set; }
    }
}