using Microsoft.EntityFrameworkCore;
using NK.Core.Model;
using NK.Core.Model.Entities;

namespace NK.Core.DataAccess.Repository
{
    public class SizeRepository : ISizeRepository
    {
        private readonly AppDbContext _appDbContext;
        public SizeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Size>> GetAllSizeAsync(CancellationToken cancellationToken = default)
        {
            var sizeList = await _appDbContext.Sizes.OrderBy(p => p.NumberSize).ToListAsync(cancellationToken);
            return sizeList;
        }

        public async Task<Size> GetByIdSizeAsync(string id, CancellationToken cancellationToken = default)
        {
            var size = await _appDbContext.Sizes.FirstOrDefaultAsync(e => e.Id == id);
            return size;
        }
        public async Task<Size> GetByNumberSizeAsync(int numberSize, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Sizes.FirstOrDefaultAsync(p => p.NumberSize == numberSize);
        }

        public async Task AddSize(Size size)
        {
            await _appDbContext.Sizes.AddAsync(size);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteSize(string id)
        {
            var size = await GetByIdSizeAsync(id);

            if (size == null) return false;

            _appDbContext.Sizes.Remove(size);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdateSize(Size size)
        {
            _appDbContext.Sizes.Update(size);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
