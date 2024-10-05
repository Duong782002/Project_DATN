using System.ComponentModel.DataAnnotations;

namespace NK.Core.Model.Entities
{
    public class Province
    {
        [Key]
        public int Province_id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
