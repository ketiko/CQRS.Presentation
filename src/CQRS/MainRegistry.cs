using CQRS.Domain;
using EventStore;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using PetaPoco;
using StructureMap.Configuration.DSL;

namespace CQRS
{
    public class MainRegistry : Registry
    {
        public MainRegistry()
        {
            //var database = new Database("DB");
            //var dispatcher = new EventDispatcher(new EmployeeMovedEventHandler(database));

            //For<Database>()
            //    .Use(database);

            //var store = Wireup.Init()
            //    .UsingSqlPersistence("DB")
            //    .InitializeStorageEngine()
            //    .UsingJsonSerialization()
            //    .UsingSynchronousDispatchScheduler()
            //    .DispatchTo(dispatcher)
            //    .Build();

            //For<IStoreEvents>()
            //    .Singleton()
            //    .Use(store);

            //For<IRepository<Employee>>()
            //    .Use<Repository<Employee

            For<IRepository>()
                .Use<Repository>();

            For<ISession>()
                .Use(() => CreateSessionFactory().OpenSession());
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(MsSqlConfiguration.MsSql2008
              .ConnectionString(b => b.FromConnectionStringWithKey("DB"))
              )
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<MainRegistry>())
              .BuildSessionFactory();
        }
    }
}