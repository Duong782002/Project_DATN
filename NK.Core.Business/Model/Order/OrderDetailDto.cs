namespace NK.Core.Business.Model.Order
{
    public class OrderDetailDto
    {
        public List<OrderDetailItemDto>? Orders { get; set; }
    }

    public class OrderDetailItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal DiscountRate { get; set; }
    }
}
