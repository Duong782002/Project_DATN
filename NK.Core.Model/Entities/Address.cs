namespace NK.Core.Model.Entities
{
    public class Address
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? FullName { get; set; }
        public string? AddressLine { get; set; }
        public int CityCode { get; set; }
        public int ProvinceCode { get; set; }
        public string WardCode { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool SetAsDefault { get; set; }
        public string? UserId { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
