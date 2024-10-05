using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public interface IProvinceService
    {
        Task<IEnumerable<Province>> GetAllProvince();
    }
}
