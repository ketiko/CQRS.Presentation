using System.Collections.Generic;
using CQRS.Domain.Events;
using NHibernate;
using NHibernate.Linq;

namespace CQRS.Domain
{
    public interface IRepository<T> where T : AggregateRoot
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Save(T item);
    }

    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        readonly ISession _session;
        readonly IEventStore _eventStore;

        public Repository(ISession session, IEventStore eventStore)
        {
            _session = session;
            _eventStore = eventStore;
        }

        public IEnumerable<T> GetAll()
        {
            return _session.Query<T>();
        }

        public T Get(int id)
        {
            return _session.Get<T>(id);
        }

        public void Save(T item)
        {
            using (var tx = _session.BeginTransaction())
            {
                _eventStore.Append(item.GetUncommitedChanges());
                item.CommitChanges();
                _session.Save(item);

                tx.Commit();
            }
        }
    }
}