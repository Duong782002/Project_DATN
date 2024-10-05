namespace NK.Core.Business.Model.Product
{
    public class ProductTopSales
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public decimal TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
