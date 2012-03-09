using System.Collections.Generic;

namespace CQRS.Employees
{
    public class IndexViewModel
    {
        public IEnumerable<EmployeeDTO> Employees { get; set; }
    }
}