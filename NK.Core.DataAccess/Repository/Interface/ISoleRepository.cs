using NK.Core.Model.Entities;
using System.Linq.Expressions;

namespace NK.Core.DataAccess.Repository
{
    public interface ISoleRepository
    {
        Task<Sole> GetByIdAsync(string id);
        Task<IEnumerable<Sole>> GetAllAsync();
        Task<IEnumerable<Sole>> FindAsync(Expression<Func<Sole, bool>> predicate);
        Task<Sole> GetSoleByNameAsync(string name);
        Task AddAsync(Sole sole);
        Task UpdateAsync(Sole sole);
        Task RemoveAsync(Sole sole);
    }
}
