namespace CQRS.Domain
{
    public class EmployeeMovedEvent
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
}