using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Order
{
    public class OrderCreateDto
    {
        public string UserId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public PaymentMethod Payment { get; set; }
        public int Province { get; set; }
        public string Provincename { get; set; } = string.Empty;
        public int District { get; set; }
        public string DistrictName { get; set; } = string.Empty;
        public string Ward { get; set; } = string.Empty;
        public string WardName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }

        public List<OrderItemInputDto> OrderItems { get; set; } = new();
    }
}
