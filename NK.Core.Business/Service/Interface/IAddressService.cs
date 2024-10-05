using NK.Core.Business.Model.Address;
using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public interface IAddressService
    {
        Task<Address> AddNew(AddressAPI address);
        Task<IEnumerable<AddressDto>> GetByUserId(string id);
        Task Update(AddressUpdateAPI address);
    }
}
