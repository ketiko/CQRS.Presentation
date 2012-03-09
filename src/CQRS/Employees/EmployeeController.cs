using System.Linq;
using CQRS.Domain;
using FubuMVC.Core.Continuations;
using PetaPoco;

namespace CQRS.Employees
{
    public class EmployeeController
    {
        readonly IRepository<Employee> _repository;
        readonly Database _db;

        public EmployeeController(IRepository<Employee> repository)
        {
            _repository = repository;
            _db = new Database("DB");
        }

        public IndexViewModel Index()
        {
            var employees = _db.Query<EmployeeDTO>("Select * From ViewModel.USEmployee")
                .Take(10);

            return new IndexViewModel
            {
                Employees = employees
            };
        }

        public EditViewModel Edit(EditInputModel input)
        {
            return _db.Single<EditViewModel>("Select * From ViewModel.USEmployeeAddress WHERE EmployeeId= @0", input.Id);
        }

        public FubuContinuation Save(SaveInputModel input)
        {
            var employee = _repository.Get(input.Id);

            employee.MoveEmployeeToAddress(input.Street, input.City);

            _repository.Save(employee);

            return FubuContinuation.RedirectTo<EmployeeController>(x => x.Index());
        }
    }
}