using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NK.Core.Model.Entities
{
    [Table("Stocks")]
    public class Stock
    {
        [Key]
        public string StockId { get; set; } = Guid.NewGuid().ToString();
        public int UnitInStock { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string SizeId { get; set; } = string.Empty;
        public virtual Product? Product { get; set; }
        public virtual Size? Size { get; set; }
    }
}
