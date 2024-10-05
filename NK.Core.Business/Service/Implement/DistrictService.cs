using Microsoft.EntityFrameworkCore;
using NK.Core.Model;
using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public class DistrictService : IDistrictService
    {
        private readonly AppDbContext _dbContext;

        public DistrictService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<District>> GetAllDistrict(int provinceId)
        {
            return await _dbContext.District.Where(p => p.Province_id == provinceId).ToListAsync();
        }
    }
}
