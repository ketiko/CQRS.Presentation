using System.Collections.Generic;

namespace CQRS.Domain.Events
{
    public interface IEventStore
    {
        void Append(IEnumerable<IEvent> events);
    }

    public class EventStore : IEventStore
    {
        public void Append(IEnumerable<IEvent> events)
        {
            foreach (var e in events)
            {
                //Append event to log

                //Raise to event handlers
                var movedEvent = e as EmployeeMovedEvent;
                if (movedEvent == null) continue;

                var eventHandler = new EmployeeMovedEventHandler();
                eventHandler.Handle(movedEvent);
            }
        }
    }
}