using Microsoft.EntityFrameworkCore;
using NK.Core.Model;
using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public class WardsService : IWardsService
    {
        private readonly AppDbContext _dbContext;

        public WardsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Wards>> GetAllWards(int districtId)
        {
            return await _dbContext.Wards.Where(p => p.District_id == districtId).ToListAsync();    
        }
    }
}
