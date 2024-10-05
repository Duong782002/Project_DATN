using System.ComponentModel.DataAnnotations.Schema;

namespace NK.Core.Model.Entities
{
    [Table("Categories")]
    public class Category
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public virtual List<Product>? Products { get; set; }
    }
}
