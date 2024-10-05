using System.ComponentModel.DataAnnotations.Schema;

namespace NK.Core.Model.Entities
{
    [Table("OrderItems")]
    public class OrderItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string OrderId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public string SizeId { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Product? Product { get; set; }
        public Size? Size { get; set; }
        public Order? Order { get; set; }
    }
}
