using Microsoft.EntityFrameworkCore;
using NK.Core.DataAccess.Repository;
using NK.Core.Model;
using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly AppDbContext _dbContext;

        public BrandService(IBrandRepository brandRepository, AppDbContext dbContext)
        {
            _brandRepository = brandRepository;
            _dbContext = dbContext;
        }

        public async Task AddBrandAsync(Brand brand)
        {
            await _brandRepository.AddAsync(brand);
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task<Brand> GetBrandByIdAsync(string id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task<Brand> GetByNameBrandAsync(string name)
        {
            return await _brandRepository.GetByNameMaterialAsync(name);
        }

        public async Task RemoveBrandAsync(Brand brand)
        {
            await _brandRepository.RemoveAsync(brand);
        }

        public async Task UpdateBrandAsync(Brand brand)
        {
            await _brandRepository.UpdateAsync(brand);
        }
        public async Task<IEnumerable<Brand>> GetBrandCount(int count)
        {
            var brands = await _dbContext.Brands
                                    .Take(count)
                                    .ToListAsync();

            return brands;
        }
    }
}
