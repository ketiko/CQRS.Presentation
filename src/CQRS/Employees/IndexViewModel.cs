using System.Collections.Generic;

namespace CQRS.Employees
{
    public class IndexViewModel
    {
        public IEnumerable<Domain.Employee> Employees { get; set; }
    }
}