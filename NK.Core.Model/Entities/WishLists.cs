using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NK.Core.Model.Entities
{
    public class WishLists
    {
        [Key]
        public string ProductsId { get; set; } = string.Empty;

        [Key]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("ProductsId")]
        public Product? Product { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser? AppUser { get; set; }
    }
}
