using Microsoft.AspNetCore.Http;
using NK.Core.Business.Model.Products;
using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Product
{
    public class ProductView
    {
        public string Id { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public DiscountType DiscountType { get; set; }
        public string SoleId { get; set; } = string.Empty;
        public string SoleName { get; set; } = string.Empty;
        public string MaterialId { get; set; } = string.Empty;
        public string MaterialName { get; set; } = string.Empty;
        public string BrandId { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal RetailPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal TotalQuantity { get; set; }
        public bool Heart { get; set; }
        public Status Status { get; set; } = Status.ACTIVE;
        public List<StockDto>? Stocks { get; set; }
        public List<ProductImageView>? Images { get; set; }

    }
}
