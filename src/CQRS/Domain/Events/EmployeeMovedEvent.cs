namespace CQRS.Domain.Events
{
    public interface IEvent
    {
        int Id { get; set; }
    }

    public class EmployeeMovedEvent : IEvent
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
}