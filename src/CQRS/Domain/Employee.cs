using System.Collections.Generic;
using System.Linq;

namespace CQRS.Domain
{
    public class Employee
    {
        public virtual int Id { get; set; }
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

        void ApplyChange(EmployeeMovedEvent e)
        {
            EventStore.Append(e);
            Apply(e);
        }

        void Apply(EmployeeMovedEvent e)
        {
            var address = Addresses.First();

            address.AddressLine1 = e.Street;
            address.City = e.City;
        }
    }
}