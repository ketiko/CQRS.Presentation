using CQRS.Domain;
using CQRS.Domain.Events;
using EventStore;
using PetaPoco;
using StructureMap.Configuration.DSL;

namespace CQRS
{
    public class MainRegistry : Registry
    {
        public MainRegistry()
        {
            var database = new Database("DB");
            var dispatcher = new EventDispatcher(new EmployeeMovedEventHandler(database));

            For<Database>()
                .Use(database);

            var store = Wireup.Init()
                .UsingSqlPersistence("DB")
                .InitializeStorageEngine()
                .UsingJsonSerialization()
                .UsingSynchronousDispatchScheduler()
                .DispatchTo(dispatcher)
                .Build();

            For<IStoreEvents>()
                .Singleton()
                .Use(store);


            For<IRepository<Employee>>()
                .Use<Repository<Employee>>();
        }
    }
}