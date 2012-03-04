namespace CQRS.Domain
{
    public class Address
    {
        public virtual int Id { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string City { get; set; }
        public virtual StateProvince StateProvince { get; set; }
    }
}