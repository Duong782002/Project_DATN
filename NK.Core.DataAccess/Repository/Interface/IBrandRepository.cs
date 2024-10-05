using NK.Core.Model.Entities;

namespace NK.Core.DataAccess.Repository
{
    public interface IBrandRepository
    {
        Task<Brand> GetByIdAsync(string id);
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<Brand> GetByNameMaterialAsync(string name);
        Task AddAsync(Brand material);
        Task UpdateAsync(Brand material);
        Task RemoveAsync(Brand material);
    }
}
