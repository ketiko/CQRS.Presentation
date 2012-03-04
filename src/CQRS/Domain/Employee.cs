using System.Collections.Generic;

namespace CQRS.Domain
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual Person Person { get; set; }
        public virtual IList<Address> Addresses { get; set; }
    }
}