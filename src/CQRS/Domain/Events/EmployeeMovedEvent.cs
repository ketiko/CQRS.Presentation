namespace CQRS.Domain.Events
{
    public interface IEvent
    {
        int Id { get; set; }
    }
}