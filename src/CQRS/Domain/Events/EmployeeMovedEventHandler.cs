using PetaPoco;

namespace CQRS.Domain.Events
{
    public interface IEventHandler<in T>
    {
        void Handle(T e);
    }

    public class EmployeeMovedEventHandler : IEventHandler<EmployeeMovedEvent>
    {
        readonly Database _db;

        public EmployeeMovedEventHandler(Database db)
        {
            _db = db;
        }

        public void Handle(EmployeeMovedEvent e)
        {
            using (var tx = _db.GetTransaction())
            {
                _db.Update("ViewModel.USEmployeeAddress", "EmployeeId", new
                {
                    EmployeeId = e.Id,
                    e.Street,
                    e.City
                });

                tx.Complete();
            }
        }
    }
}