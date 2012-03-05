using System.Collections.Generic;

namespace CQRS.Employee
{
    public class IndexViewModel
    {
        public IEnumerable<EmployeeDTO> Employees { get; set; }
    }
}