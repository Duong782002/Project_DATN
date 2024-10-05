namespace NK.Core.Model.Entities
{
    public class Contract
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        public virtual AppUser? AppUser { get; set; }
    }
}
