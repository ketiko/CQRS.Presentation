namespace CQRS.Domain
{
    public class EventStore
    {
        public static void Append(EmployeeMovedEvent e)
        {
            //Persist

            //Raise to event handlers
            var h = new EmployeeMovedEventHandler();
            h.Handle(e);
        }
    }
}