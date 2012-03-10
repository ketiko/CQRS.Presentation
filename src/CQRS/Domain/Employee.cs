using System.Collections.Generic;
using CQRS.Domain.Events;

namespace CQRS.Domain
{
    public abstract class AggregateRoot
    {
        public int Id { get; set; }
        readonly List<IEvent> _changes;

        protected AggregateRoot()
        {
            _changes = new List<IEvent>();
        }

        protected void ApplyChange(EmployeeMovedEvent e)
        {
            ApplyChange(e, true);
        }

        protected void ApplyChange(EmployeeMovedEvent e, bool isNew)
        {
            if (isNew)
            {
                _changes.Add(e);
            }

            Apply(e);
        }

        protected abstract void Apply(EmployeeMovedEvent e);

        public IEnumerable<IEvent> GetUncommitedChanges()
        {
            return _changes.AsReadOnly();
        }

        public void CommitChanges()
        {
            _changes.Clear();
        }

        public void LoadFromEvents(IEnumerable<IEvent> events)
        {
            foreach (var e in events)
            {
                var movedEvent = e as EmployeeMovedEvent;
                if (movedEvent != null)
                {
                    ApplyChange(movedEvent);
                }
            }
        }
    }

    public class Employee : AggregateRoot
    {
        Address _address;

        public virtual void MoveEmployeeToAddress(string street, string city)
        {
            ApplyChange(new EmployeeMovedEvent
            {
                Id = Id,
                Street = street,
                City = city
            });
        }

        protected override void Apply(EmployeeMovedEvent e)
        {
            _address = new Address(e.Street, e.City);
        }
    }
}