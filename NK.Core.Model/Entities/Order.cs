using NK.Core.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace NK.Core.Model.Entities
{
    [Table("Orders")]
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? CustomerName { get; set; }
        public string? AddressName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Note { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethod Payment { get; set; }
        public StatusOrder CurrentStatus { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime PassivedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? UserId { get; set; }
        public string? AddressId { get; set; }

        public virtual AppUser? AppUser { get; set; }
        public virtual Address? Address { get; set; }
        public virtual IEnumerable<OrderItem>? OrderItems { get; set; }
        public virtual List<OrderStatus>? OrderStatuses { get; set; }
    }
}
