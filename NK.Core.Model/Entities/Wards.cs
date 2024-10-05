using System.ComponentModel.DataAnnotations;

namespace NK.Core.Model.Entities
{
    public class Wards
    {
        [Key]
        public int Washs_id { get; set; }
        public int District_id { get; set; }
        public string Name { get; set;} = string.Empty;
    }
}
