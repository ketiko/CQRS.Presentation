using CQRS.Domain.Events;
using EventStore;
using EventStore.Dispatcher;

namespace CQRS
{
    public class EventDispatcher : IDispatchCommits
    {
        readonly IEventHandler<EmployeeMovedEvent> _handler;

        public EventDispatcher(IEventHandler<EmployeeMovedEvent> handler)
        {
            _handler = handler;
        }

        public void Dispatch(Commit commit)
        {
            commit.Events.ForEach(e =>
                                  _handler.Handle((EmployeeMovedEvent)e.Body));
        }

        public void Dispose() { }
    }
}