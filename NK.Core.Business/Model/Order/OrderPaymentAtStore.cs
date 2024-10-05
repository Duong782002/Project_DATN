using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Order
{
    public class OrderPaymentAtStore
    {
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<OrderItemList>? OrderItems { get; set; }

    }

    public class OrderItemList
    {
        public string ProductId { get; set; } = string.Empty;
        public string SizeId { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
    } 
}
