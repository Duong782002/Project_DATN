using NK.Core.Model.Enums;

namespace NK.Core.Model.Entities
{
    public class ShoppingCartItems
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public StatusCard Status { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string SizeId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        public virtual AppUser? AppUser { get; set; }
        public virtual Product? Product { get; set; }
    }
}
