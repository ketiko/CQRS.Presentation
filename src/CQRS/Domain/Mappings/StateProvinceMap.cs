using FluentNHibernate.Mapping;

namespace CQRS.Domain.Mappings
{
    public class StateProvinceMap : ClassMap<StateProvince>
    {
        public StateProvinceMap()
        {
            Table("Person.StateProvince");
            Id(x => x.Id, "StateProvinceId");
            Map(x => x.StateProvinceCode);
            Map(x => x.CountryRegionCode);
        }
    }
}