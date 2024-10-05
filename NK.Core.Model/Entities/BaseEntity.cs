using NK.Core.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace NK.Core.Model.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime? ModifiedDate { get; set; }
        public string? Name { get; set; }
        public Status Status { get; set; } = Status.ACTIVE;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
