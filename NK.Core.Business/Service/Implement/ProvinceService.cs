using Microsoft.EntityFrameworkCore;
using NK.Core.Model;
using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public class ProvinceService : IProvinceService
    {
        private readonly AppDbContext _dbContext;

        public ProvinceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Province>> GetAllProvince()
        {
            return await _dbContext.Province.ToListAsync();
        }
    }
}
