using NK.Core.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace NK.Core.Model.Entities
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        public decimal RetailPrice { get; set; }
        public string? Description { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal? TaxRate { get; set; }
        public int FirstQuantity { get; set; }
        public DiscountType DiscountType { get; set; }
        public Weather weather { get; set; }
        public byte[]? ProductImage { get; set; }
        public string SoleId { get; set; } = string.Empty;
        public string MaterialId { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public string BrandId { get; set; } = string.Empty;

        public virtual Material? Material { get; set; }
        public virtual Sole? Sole { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Category? Category { get; set; } 
        public virtual List<Stock>? Stocks { get; set; }
        public virtual IEnumerable<OrderItem>? OrderItems { get; set; }
        public virtual ICollection<ShoppingCartItems>? ShoppingCartItems { get; set; }
    }
}
