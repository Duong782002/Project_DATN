namespace NK.Core.Business.Model.Order
{
    public class OrderItemDto
    {
        public string OrderId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; }
        public string SizeId { get; set; } = string.Empty;
        public int NumberSize { get; set; }
        public decimal Quantity { get; set; } 
        public decimal Price { get; set; }
    }
}
