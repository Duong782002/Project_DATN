using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NK.Core.Business.Model.Address
{
    public class AddressDto
    {
        public string Id { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public int CityCode { get; set; }
        public int ProvinceCode { get; set; }
        public string WardCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool SetAsDefault { get; set; }
    }
}
