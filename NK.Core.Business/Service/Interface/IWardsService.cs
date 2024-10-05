using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public interface IWardsService
    {
        Task<IEnumerable<Wards>> GetAllWards(int districtId);
    }
}
