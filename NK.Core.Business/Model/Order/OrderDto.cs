using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Order
{
    public class OrderDto
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string AddressName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public PaymentMethod Payment { get; set; }
        public StatusOrder CurrentStatus { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime PassivedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public List<OrderItemDto>? OrderItems { get; set; }
        public List<OrderStatusDto>? OrderStatuses { get; set; }
    }
}
