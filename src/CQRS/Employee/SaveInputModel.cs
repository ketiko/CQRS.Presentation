namespace CQRS.Employee
{
    public class SaveInputModel
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}