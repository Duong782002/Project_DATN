using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public interface IBrandService
    {
        Task<Brand> GetBrandByIdAsync(string id);
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<Brand> GetByNameBrandAsync(string name);
        Task AddBrandAsync(Brand brand);
        Task UpdateBrandAsync(Brand brand);
        Task RemoveBrandAsync(Brand brand);
        Task<IEnumerable<Brand>> GetBrandCount(int count);
    }
}
