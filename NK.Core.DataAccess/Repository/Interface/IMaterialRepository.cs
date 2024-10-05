using NK.Core.Model.Entities;
using System.Linq.Expressions;

namespace NK.Core.DataAccess.Repository
{
    public interface IMaterialRepository
    {
        Task<Material> GetByIdAsync(string id);
        Task<IEnumerable<Material>> GetAllAsync();
        Task<IEnumerable<Material>> FindAsync(Expression<Func<Material, bool>> predicate);
        Task<Material> GetByNameMaterialAsync(string name);
        Task AddAsync(Material material);
        Task UpdateAsync(Material material);
        Task RemoveAsync(Material material);
    }
}
