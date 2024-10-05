using NK.Core.Business.Model;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;

namespace NK.Core.Business.Service
{
    public interface IStockService
    {
        Task<Stock> GetStockByRelation(string productId, string colorId, string sizeId);
        Task<IEnumerable<Stock>> GetAllStocksAsync();
        Task<IEnumerable<Stock>> GetStockByIdAsync(string productId);
        Task AddStockAsync(Stock stock);
        void UpdateStockRangeAsync(List<Stock> stocks);
        Task DeleteStockAsync(string productId);
        Task<decimal> TotalQuantityProduct(string id);
        Task<StockQuantity> QuantityInventoryMovement();
        List<ProductByStatus> GetProductQuantityByOrderStatus();
        List<StockQuantityForMonth> GetSixMonthProductStats();
        int GetQuantityForTime(DateTime startDate, DateTime endDate, Activity activity);
    }
}
