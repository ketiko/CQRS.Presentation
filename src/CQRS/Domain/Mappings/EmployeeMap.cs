using FluentNHibernate.Mapping;

namespace CQRS.Domain.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("HumanResources.Employee");
            Id(x => x.Id, "EmployeeId");
            HasOne(x => x.Person)
                .Cascade.SaveUpdate();
            HasManyToMany(x => x.Addresses)
                .Table("HumanResources.EmployeeAddress")
                .ParentKeyColumn("EmployeeId")
                .ChildKeyColumn("AddressId")
                .Cascade.SaveUpdate();
        }
    }
}