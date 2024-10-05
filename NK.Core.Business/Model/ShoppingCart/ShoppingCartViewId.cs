namespace NK.Core.Business.Model.ShoppingCart
{
    public class ShoppingCartViewId
    {
        public string Id { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int NumberSize { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal Price { get; set; }
    }
}
