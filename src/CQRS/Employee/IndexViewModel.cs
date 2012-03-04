using System.Collections.Generic;

namespace CQRS.Employee
{
    public class IndexViewModel
    {
        public List<Domain.Employee> Employees { get; set; }
    }
}