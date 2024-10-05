using System.ComponentModel.DataAnnotations;

namespace NK.Core.Model.Entities
{
    public class District
    {
        [Key]
        public int District_id { get; set; }
        public int Province_id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
