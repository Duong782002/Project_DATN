using NK.Core.Model.Entities;

namespace NK.Core.DataAccess.Repository
{
    public interface IStockRepository
    {
        void UpdateRange(List<Stock> Stocks);
        Task<Stock> SelectByVariantId(string productId, string colorId, string sizeId);
        Task<IEnumerable<Stock>> SelectById(string productId);
        Task<IEnumerable<Stock>> GetAllAsync();
        Task AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task DeleteByProductIdAsync(string productId);
        Task<Stock>? GetStockByProductAndSize(string productId, string sizeId);
    }
}
