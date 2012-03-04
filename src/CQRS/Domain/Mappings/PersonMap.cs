using FluentNHibernate.Mapping;

namespace CQRS.Domain.Mappings
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Table("Person.Contact");
            Id(x => x.Id, "ContactId");
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.EmailAddress);
        }
    }
}