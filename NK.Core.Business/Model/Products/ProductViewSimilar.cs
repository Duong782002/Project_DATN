namespace NK.Core.Business.Model.Products
{
    public class ProductViewSimilar
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal TotalQuantity { get; set; }
        public string Image { get; set; } = string.Empty;
        public decimal DiscountRate {  get; set; }  
    }
}
