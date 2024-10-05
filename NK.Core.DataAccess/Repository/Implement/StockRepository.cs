using NK.Core.Model.Entities;
using NK.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace NK.Core.DataAccess.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock> SelectByVariantId(string productId, string colorId, string sizeId)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == productId && s.SizeId == sizeId);
        }

        public async Task AddAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public void UpdateRange(List<Stock> Stocks)
        {
            _context.Stocks.UpdateRange(Stocks);
        }

        public async Task DeleteByProductIdAsync(string productId)
        {

            var stocksToDelete = await _context.Stocks.Where(stock => stock.ProductId == productId).ToListAsync();
            if (stocksToDelete == null || stocksToDelete.Count == 0)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }
            _context.Stocks.RemoveRange(stocksToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Stock>> SelectById(string productId)
        {
            return await _context.Stocks.Where(s => s.ProductId == productId).ToListAsync();
        }

        public async Task<Stock>? GetStockByProductAndSize(string productId, string sizeId)
        {
            return await _context.Stocks.Where(p => p.ProductId == productId && p.SizeId == sizeId).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
        }
    }
}
