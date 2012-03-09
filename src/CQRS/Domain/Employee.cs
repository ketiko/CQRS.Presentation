using System.Collections.Generic;
using System.Linq;
using CQRS.Domain.Events;

namespace CQRS.Domain
{
    public abstract class AggregateRoot
    {
        public virtual int Id { get; set; }
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

        public virtual IEnumerable<IEvent> GetUncommitedChanges()
        {
            return _changes.AsReadOnly();
        }

        public virtual void CommitChanges()
        {
            _changes.Clear();
        }
    }

    public class Employee : AggregateRoot
    {
        protected internal virtual Person Person { get; set; }
        protected internal virtual IList<Address> Addresses { get; set; }

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
            var address = Addresses.First();

            address.AddressLine1 = e.Street;
            address.City = e.City;
        }
    }
}