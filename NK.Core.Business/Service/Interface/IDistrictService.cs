using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public interface IDistrictService
    {
        Task<IEnumerable<District>> GetAllDistrict(int provinceId);
    }
}
