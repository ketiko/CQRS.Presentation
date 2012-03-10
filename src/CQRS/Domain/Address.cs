namespace CQRS.Domain
{
    public class Address
    {
        public Address(string street, string city)
        {
            Street = street;
            City = city;
        }

        public string Street { get; private set; }
        public string City { get; private set; }
    }
}