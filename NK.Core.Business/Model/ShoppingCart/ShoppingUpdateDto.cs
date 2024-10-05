namespace NK.Core.Business.Model.ShoppingCart
{
    public class ShoppingUpdateDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public string SizeId {  get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
