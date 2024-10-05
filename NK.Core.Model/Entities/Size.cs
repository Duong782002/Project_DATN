using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NK.Core.Model.Entities
{
    [Table("Sizes")]
    public class Size
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required(ErrorMessage = "Số size là bắt buộc")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Size phải là số")]
        public int NumberSize { get; set; }
        public virtual List<Stock>? Stocks { get; set; }
    }
}
