using PetaPoco;

namespace CQRS.Domain.Events
{
    public class EmployeeMovedEventHandler
    {
        readonly Database db;

        public EmployeeMovedEventHandler()
        {
            db = new Database("DB");
        }

        public void Handle(EmployeeMovedEvent e)
        {
            using (var tx = db.GetTransaction())
            {
                db.Update("ViewModel.USEmployeeAddress", "EmployeeId", new
                {
                    EmployeeId = e.Id,
                    e.Street,
                    e.City
                }, new[] { "Street", "City" });

                tx.Complete();
            }
        }
    }
}