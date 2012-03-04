namespace CQRS.Domain
{
    public class StateProvince
    {
        public virtual int Id { get; set; }
        public virtual string StateProvinceCode { get; set; }
        public virtual string CountryRegionCode { get; set; }
    }
}