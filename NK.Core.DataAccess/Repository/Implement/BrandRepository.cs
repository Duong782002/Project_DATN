using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NK.Core.Model;
using NK.Core.Model.Entities;

namespace NK.Core.DataAccess.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _dbContext;
        public BrandRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Brand material)
        {
            await _dbContext.Brands.AddAsync(material);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _dbContext.Brands.ToListAsync();
        }

        public async Task<Brand> GetByIdAsync(string id)
        {
            return await _dbContext.Brands.FindAsync(id);
        }

        public async Task<Brand> GetByNameMaterialAsync(string name)
        {
            return await _dbContext.Brands.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task RemoveAsync(Brand material)
        {
            _dbContext.Brands.Remove(material);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Brand material)
        {
            _dbContext.Brands.Update(material);
            await _dbContext.SaveChangesAsync();
        }
    }
}
