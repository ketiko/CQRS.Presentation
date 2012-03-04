using FluentNHibernate.Mapping;

namespace CQRS.Domain.Mappings
{
    public class AddressMap : ClassMap<Address>
    {
        public AddressMap()
        {
            Table("Person.Address");
            Id(x => x.Id, "AddressId");
            Map(x => x.City);
            Map(x => x.AddressLine1);
            References(x => x.StateProvince, "StateProvinceId")
                .Cascade.SaveUpdate();
        }
    }
}