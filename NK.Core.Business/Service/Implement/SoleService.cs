using NK.Core.DataAccess.Repository;
using NK.Core.Model.Entities;
using System.Linq.Expressions;

namespace NK.Core.Business.Service
{
    public class SoleService : ISoleService
    {
        private readonly ISoleRepository _soleRepository;

        public SoleService(ISoleRepository soleRepository)
        {
            _soleRepository = soleRepository;
        }
        public async Task<Sole> GetSoleByIdAsync(string id)
        {
            return await _soleRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Sole>> GetAllSolesAsync()
        {
            return await _soleRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Sole>> FindSolesAsync(Expression<Func<Sole, bool>> predicate)
        {
            return await _soleRepository.FindAsync(predicate);
        }

        public async Task AddSoleAsync(Sole sole)
        {
            await _soleRepository.AddAsync(sole);
        }

        public async Task UpdateSoleAsync(Sole sole)
        {
            await _soleRepository.UpdateAsync(sole);
        }

        public async Task RemoveSoleAsync(Sole sole)
        {
            await _soleRepository.RemoveAsync(sole);
        }

        public async Task<Sole> GetSoleByNameAsync(string name)
        {
            return await _soleRepository.GetSoleByNameAsync(name);
        }
    }
}
