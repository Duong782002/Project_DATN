using NK.Core.Model.Entities;
using System.Linq.Expressions;

namespace NK.Core.Business.Service
{
    public interface IMaterialService
    {
        Task<Material> GetMaterialByIdAsync(string id);
        Task<IEnumerable<Material>> GetAllMaterialsAsync();
        Task<IEnumerable<Material>> FindMaterialsAsync(Expression<Func<Material, bool>> predicate);
        Task<Material> GetByNameMaterialAsync(string name);
        Task AddMaterialAsync(Material material);
        Task UpdateMaterialAsync(Material material);
        Task RemoveMaterialAsync(Material material);
    }
}
