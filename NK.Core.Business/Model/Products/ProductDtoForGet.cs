using Microsoft.AspNetCore.Http;
using NK.Core.Business.Model.Products;
using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Product
{
    public class ProductDtoForGet
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal RetailPrice { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public DiscountType DiscountType { get; set; }
        public IFormFile? Image { get; set; }
        public string SoleId { get; set; } = string.Empty;
        public string MaterialId { get; set; } = string.Empty;
        public string BrandId { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.ACTIVE;
        public List<StockDto>? Stocks { get; set; }
    }
}
