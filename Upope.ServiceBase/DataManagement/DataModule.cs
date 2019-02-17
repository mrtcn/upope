using System;

namespace Upope.ServiceBase.DataManagement {
    public class DataModule : Module {
        private readonly Type _entitiesType;

        public DataModule(Type entitiesType) {
            if (entitiesType == null) {
                throw new ArgumentNullException("entitiesType");
            }

            _entitiesType = entitiesType;
        }

        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType(_entitiesType).As<IObjectContextAdapter>().InstancePerRequest();
            builder.RegisterType<ContextAdaptor>().As<IObjectSetFactory, IObjectContext>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();
        }
    }
}