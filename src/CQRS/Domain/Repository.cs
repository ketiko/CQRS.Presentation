using System.Collections.Generic;
using NHibernate;
using NHibernate.Linq;

namespace CQRS.Domain
{
    public class Repository : IRepository
    {
        readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _session.Query<T>();
        }

        public T Get<T>(int id)
        {
            return _session.Get<T>(id);
        }

        public void Save<T>(T item)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(item);

                tx.Commit();
            }
        }
    }
}