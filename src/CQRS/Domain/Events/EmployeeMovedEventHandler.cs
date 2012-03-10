namespace CQRS.Domain.Events
{
    public interface IEventHandler<in T>
    {
        void Handle(T e);
    }
}