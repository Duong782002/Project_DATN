using NK.Core.Model.Entities;

namespace NK.Core.DataAccess.Repository
{
    public interface IAddressRepository
    {
        Task Add(Address address);
        Task<IEnumerable<Address>> GetByUserId(string id);
        void UpdateRange(List<Address> addresses);
        Task Update(string id, Address address);
    }
}
