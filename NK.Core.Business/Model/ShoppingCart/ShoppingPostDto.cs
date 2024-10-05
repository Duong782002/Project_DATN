using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.ShoppingCart
{
    public class ShoppingPostDto
    {
        public string UserId {  get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public StatusCard Status { get; set; }
        public int Quantity { get; set; }
        public string SizeId { get; set; } = string.Empty;
    }
}
