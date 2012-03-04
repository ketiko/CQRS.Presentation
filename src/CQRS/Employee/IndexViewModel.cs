using System.Collections.Generic;

namespace CQRS.Employee
{
    public class IndexViewModel
    {
        public IEnumerable<Domain.Employee> Employees { get; set; }
    }
}