using CQRS.Domain;
using CQRS.Domain.Events;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace CQRS
{
    public class MainRegistry : Registry
    {
        public MainRegistry()
        {
            For<IEventStore>()
                .Use<EventStore>();

            For<IRepository<Employee>>()
                .Use<Repository<Employee>>();

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