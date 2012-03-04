using System.Linq;
using CQRS.Domain;
using FubuMVC.Core.Continuations;

namespace CQRS.Employee
{
    public class EmployeeController
    {
        readonly IRepository _repository;

        public EmployeeController(IRepository repository)
        {
            _repository = repository;
        }

        public IndexViewModel Index()
        {
            var employees = _repository.GetAll<Domain.Employee>();
            var tennesseens = employees.Where(x =>
            {
                var address = x.Addresses.FirstOrDefault();
                return address != null && address.StateProvince.CountryRegionCode == "US" && address.StateProvince.StateProvinceCode.Trim() != "WA";
            }).ToList();

            return new IndexViewModel
            {
                Employees = tennesseens
            };
        }

        public EditViewModel Edit(EditInputModel input)
        {
            var employee = _repository.Get<Domain.Employee>(input.Id);
            var address = employee.Addresses.First();
            return new EditViewModel
            {
                Id = employee.Id,
                FirstName = employee.Person.FirstName,
                LastName = employee.Person.LastName,
                Street = address.AddressLine1,
                City = address.City,
                State = address.StateProvince.StateProvinceCode
            };
        }

        public FubuContinuation Save(SaveInputModel input)
        {
            var employee = _repository.Get<Domain.Employee>(input.Id);
            var address = employee.Addresses.First();
            address.AddressLine1 = input.Street;
            address.City = input.City;

            _repository.Save(address);

            return FubuContinuation.RedirectTo<EmployeeController>(x => x.Index());
        }
    }
}