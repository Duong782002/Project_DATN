using NK.Core.Model.Entities;

namespace NK.Core.DataAccess.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<Category> GetByIdAsync(string id);
        Task AddAsync(Category category);
        Task UpdateAsync(string id, Category category);
        Task DeleteAsync(string id);
        Task<Category> GetCategoryByNameAsync(string name);
    }
}
