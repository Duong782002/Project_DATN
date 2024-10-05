using NK.Core.Model.Entities;
using System.Linq.Expressions;

namespace NK.Core.Business.Service
{
    public interface ISoleService
    {
        Task<Sole> GetSoleByIdAsync(string id);
        Task<IEnumerable<Sole>> GetAllSolesAsync();
        Task<IEnumerable<Sole>> FindSolesAsync(Expression<Func<Sole, bool>> predicate);
        Task<Sole> GetSoleByNameAsync(string name);
        Task AddSoleAsync(Sole sole);
        Task UpdateSoleAsync(Sole sole);
        Task RemoveSoleAsync(Sole sole);
    }
}
