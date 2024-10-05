using Microsoft.EntityFrameworkCore;
using NK.Core.DataAccess.Repository;
using NK.Core.Model;
using NK.Core.Model.Entities;
using System.Linq.Expressions;

namespace NK.Core.Business.Service
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly AppDbContext _dbContext;

        public MaterialService(IMaterialRepository materialRepository, AppDbContext dbContext)
        {
            _materialRepository = materialRepository;
            _dbContext = dbContext;
        }

        public async Task<Material> GetMaterialByIdAsync(string id)
        {
            return await _materialRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Material>> GetAllMaterialsAsync()
        {
            return await _materialRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Material>> FindMaterialsAsync(Expression<Func<Material, bool>> predicate)
        {
            return await _materialRepository.FindAsync(predicate);
        }

        public async Task AddMaterialAsync(Material material)
        {
            await _materialRepository.AddAsync(material);
        }

        public async Task UpdateMaterialAsync(Material material)
        {
            await _materialRepository.UpdateAsync(material);
        }

        public async Task RemoveMaterialAsync(Material material)
        {
            await _materialRepository.RemoveAsync(material);
        }

        public async Task<Material> GetByNameMaterialAsync(string name)
        {
            return await _materialRepository.GetByNameMaterialAsync(name);
        }
    }
}
