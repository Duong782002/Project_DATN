using NK.Core.Model.Entities;

namespace NK.Core.Business.Model
{
    public class ShoppingCartDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Image { get;set; } = string.Empty;
        public decimal DiscountRate { get; set; }
        public List<StockDto> Stocks { get; set; } = new();
    }
}
