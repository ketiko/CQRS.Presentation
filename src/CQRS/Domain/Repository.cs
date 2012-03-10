using System;
using System.Linq;
using CQRS.Domain.Events;
using EventStore;

namespace CQRS.Domain
{
    public interface IRepository<T> where T : AggregateRoot
    {
        T Get(int id);
        void Save(T item);
    }

    public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
    {
        readonly IStoreEvents _eventStore;

        public Repository(IStoreEvents eventStore)
        {
            _eventStore = eventStore;
        }

        public T Get(int id)
        {
            var item = new T
            {
                Id = id
            };

            using (var stream = _eventStore.OpenStream(id.ToGuid(), 0, int.MaxValue))
            {
                if (stream.CommittedEvents.Count > 0)
                {
                    var events = stream.CommittedEvents.Select(x => (IEvent)x.Body);
                    item.LoadFromEvents(events);
                }
            }

            return item;
        }

        public void Save(T item)
        {
            using (var stream = _eventStore.OpenStream(item.Id.ToGuid(), 0, int.MaxValue))
            {
                var events = item.GetUncommitedChanges();
                foreach (var e in events)
                {
                    stream.Add(new EventMessage
                    {
                        Body = e
                    });
                }

                stream.CommitChanges(Guid.NewGuid());
            }

            item.CommitChanges();
        }
    }
}