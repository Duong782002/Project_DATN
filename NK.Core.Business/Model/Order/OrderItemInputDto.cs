namespace NK.Core.Business.Model.Order
{
    public class OrderItemInputDto
    {
        public string id { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int NumberSize { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
