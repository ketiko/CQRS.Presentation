using System.Collections.Generic;
using System.Linq;
using CQRS.Domain;
using FubuMVC.Core.Continuations;
using PetaPoco;

namespace CQRS.Employee
{
    public class EmployeeController
    {
        readonly IRepository _repository;
        readonly Database db;

        public EmployeeController(IRepository repository)
        {
            _repository = repository;
            db = new Database("DB");
        }

        public IndexViewModel Index()
        {
            var employees = db.Query<EmployeeDTO>("Select * From ViewModel.USEmployee")
                .Take(10);

            return new IndexViewModel
            {
                Employees = employees
            };
        }

        public EditViewModel Edit(EditInputModel input)
        {
            return db.Single<EditViewModel>("Select * From ViewModel.USEmployeeAddress WHERE EmployeeId= @0", input.Id);
        }

        public FubuContinuation Save(SaveInputModel input)
        {
            var employee = _repository.Get<Domain.Employee>(input.Id);

            employee.MoveEmployeeToAddress(input.Street, input.City);

            _repository.Save(employee);

            return FubuContinuation.RedirectTo<EmployeeController>(x => x.Index());
        }
    }
}