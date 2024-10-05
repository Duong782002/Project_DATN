using System.ComponentModel.DataAnnotations;

namespace NK.Core.Business.Model.Address
{
    public class AddressAPI
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string AddressLine { get; set; } = string.Empty;

        [Required]
        public int CityCode { get; set; }

        [Required]
        public int ProvinceCode { get; set; }

        [Required]
        public string WardCode { get; set; } = string.Empty;

        [Required]
        [StringLength(10, ErrorMessage = "Số điện thoại phải đủ 10 số")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public bool SetAsDefault { get; set; }
    }

    public class AddressUpdateAPI : AddressAPI
    {
        [Required]
        public string Id { get; set; } = string.Empty;
    }
}
