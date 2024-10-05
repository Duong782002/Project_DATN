using Microsoft.EntityFrameworkCore;
using NK.Core.Business.Model;
using NK.Core.DataAccess.Repository;
using NK.Core.Model;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;

namespace NK.Core.Business.Service
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly AppDbContext _dbContext;
        public StockService(IStockRepository stockRepository, AppDbContext dbContext)
        {
            _stockRepository = stockRepository;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Stock>> GetAllStocksAsync()
        {
            return await _stockRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Stock>> GetStockByIdAsync(string productId)
        {
            return await _stockRepository.SelectById(productId);
        }

        public async Task AddStockAsync(Stock stock)
        {
            await _stockRepository.AddAsync(stock);
        }

        public void UpdateStockRangeAsync(List<Stock> stocks)
        {
            _stockRepository.UpdateRange(stocks);
        }

        public async Task DeleteStockAsync(string productId)
        {
            await _stockRepository.DeleteByProductIdAsync(productId);

        }

        public async Task<Stock> GetStockByRelation(string productId, string colorId, string sizeId)
        {
            var result = await _stockRepository.SelectByVariantId(productId, colorId, sizeId);
            return result;
        }

        public async Task<decimal> TotalQuantityProduct(string id)
        {
            var query = await _dbContext.Stocks.Where(p => p.ProductId == id).ToListAsync();

            return query.Sum(p => p.UnitInStock);
        }

        public async Task<StockQuantity> QuantityInventoryMovement()
        {
            var productsSold = _dbContext.OrderItems
                                .Where(oi => oi.Order.DateCreated.Year == DateTime.Now.Year)
                                .GroupBy(oi => oi.ProductId)
                                .Select(g => new
                                {
                                    ProductId = g.Key,
                                    QuantitySold = g.Sum(oi => oi.Quantity)
                                })
                                .ToList();

            var stocksQuantity = _dbContext.Stocks
                                .Where(s => s.Product.CreatedDate.Year == DateTime.Now.Year)
                                .GroupBy(s => s.ProductId)
                                .Select(g => new
                                {
                                    ProductId = g.Key,
                                    QuantityStock = g.Sum(s => s.UnitInStock)
                                })
                                .ToList();

            var productsReceivedThisYear = _dbContext.Products
                                .Where(s => s.CreatedDate.Year == DateTime.Now.Year)
                                .Sum(p => p.FirstQuantity);

            var totalQuantity = productsSold.Sum(p => p.QuantitySold) + stocksQuantity.Sum(p => p.QuantityStock) + productsReceivedThisYear;

            var productsSoldPercent = (int)Math.Round((productsSold.Sum(p => p.QuantitySold) / totalQuantity) * 100);
            var productsInStockPercent = (int)Math.Round((stocksQuantity.Sum(p => p.QuantityStock) / (double)totalQuantity) * 100);
            var productsReceivedPercent = (int)Math.Round((productsReceivedThisYear / totalQuantity) * 100);

            var stockQuantity = new StockQuantity()
            {
                QuantityWarehouse = productsSold.Sum(p => p.QuantitySold),
                QuantityInventory = stocksQuantity.Sum(p => p.QuantityStock),
                QuantityImport = productsReceivedThisYear,
                PercentWarehouse = productsSoldPercent,
                PercentInventory = productsInStockPercent,
                PercentImport = productsReceivedPercent,
            };

            return stockQuantity;
        }
        public List<ProductByStatus> GetProductQuantityByOrderStatus()
        {
            var productByStatus = _dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.OrderItems != null && o.OrderItems.Any())
                .GroupBy(p => p.CurrentStatus)
                .Select(g => new ProductByStatus
                {
                    Status = g.Key,
                    Quantity = g.SelectMany(o => o.OrderItems)
                               .Sum(oi => oi.Quantity)
                })
                .ToList();

            // Duyệt qua tất cả các StatusOrder và kiểm tra xem nó có trong kết quả hay không
            foreach (StatusOrder status in Enum.GetValues(typeof(StatusOrder)))
            {
                if (!productByStatus.Any(p => p.Status == status))
                {
                    // Nếu không tìm thấy, thêm một ProductByStatus mới với Quantity là 0
                    productByStatus.Add(new ProductByStatus { Status = status, Quantity = 0 });
                }
            }

            return productByStatus;
        }
        public List<StockQuantityForMonth> GetSixMonthProductStats()
        {
            var sixMonthStats = new List<StockQuantityForMonth>();

            // Lấy ngày đầu tiên của 6 tháng gần nhất
            var sixMonthsAgo = DateTime.Now.AddMonths(-4).Date;

            // Lấy dữ liệu từ bảng OrderItem để tính số lượng sản phẩm đã bán trong mỗi tháng
            var orderItems = _dbContext.OrderItems.Include(oi => oi.Order)
                                                 .Where(oi => oi.Order.DateCreated != null && oi.Order.DateCreated >= sixMonthsAgo)
                                                 .GroupBy(oi => new { Year = oi.Order.DateCreated.Year, Month = oi.Order.DateCreated.Month })
                                                 .Select(g => new
                                                 {
                                                     Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                                                     SoldQuantity = g.Sum(oi => oi.Quantity)
                                                 })
                                                 .ToList();

            // Lấy số lượng sản phẩm trong kho trong mỗi tháng
            var stockQuantities = _dbContext.Stocks.Include(s => s.Product)
                                                  .Where(s => s.Product.CreatedDate >= sixMonthsAgo)
                                                  .GroupBy(s => new { Year = s.Product.CreatedDate.Year, Month = s.Product.CreatedDate.Month })
                                                  .Select(g => new
                                                  {
                                                      Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                                                      InStockQuantity = g.Sum(s => s.UnitInStock)
                                                  })
                                                  .ToList();

            var importQuantities = _dbContext.Products
                                                  .Where(s => s.CreatedDate >= sixMonthsAgo)
                                                  .GroupBy(s => new { Year = s.CreatedDate.Year, Month = s.CreatedDate.Month })
                                                  .Select(g => new
                                                  {
                                                      Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                                                      QuantityImport = g.Sum(s => s.FirstQuantity)
                                                  })
                                                  .ToList();

            // Tạo danh sách các tháng cần hiển thị
            var monthsToDisplay = Enumerable.Range(0, 6).Select(i => sixMonthsAgo.AddMonths(i));

            // Kết hợp dữ liệu và tính toán số lượng sản phẩm tồn kho và đã bán trong mỗi tháng
            foreach (var monthToDisplay in monthsToDisplay)
            {
                var orderItem = orderItems.FirstOrDefault(oi => oi.Date.Year == monthToDisplay.Year && oi.Date.Month == monthToDisplay.Month);
                var stockQuantity = stockQuantities.FirstOrDefault(s => s.Date.Year == monthToDisplay.Year && s.Date.Month == monthToDisplay.Month);
                var importQuantity = importQuantities.FirstOrDefault(s => s.Date.Year == monthToDisplay.Year && s.Date.Month == monthToDisplay.Month);

                // Nếu không có dữ liệu cho tháng hiện tại, gán số lượng bằng 0
                decimal soldQuantity = 0;
                decimal inStockQuantity = 0;
                decimal quantityImport = 0;

                if (orderItem != null)
                    soldQuantity = orderItem.SoldQuantity;

                if (stockQuantity != null)
                    inStockQuantity = stockQuantity.InStockQuantity;

                if (importQuantity != null)
                    quantityImport = importQuantity.QuantityImport;

                var monthStat = new StockQuantityForMonth
                {
                    Month = monthToDisplay.Month,
                    Warehouse = (int)soldQuantity,
                    Inventory = (int)inStockQuantity,
                    Import = (int)quantityImport
                };
                sixMonthStats.Add(monthStat);
            }

            return sixMonthStats;
        }

        public int GetQuantityForTime(DateTime startDate, DateTime endDate, Activity activity)
        {
            if(activity == Activity.Import)
            {
                var importQuantity = _dbContext.Products
                                                .Where(p => p.CreatedDate >= startDate && p.CreatedDate <= endDate)
                                                .Sum(p => p.FirstQuantity);

                return importQuantity;
            }
            else if(activity == Activity.Warehouse)
            {
                var soldQuantity = _dbContext.OrderItems
                                             .Where(oi => oi.Order.DateCreated >= startDate && oi.Order.DateCreated <= endDate)
                                             .Sum(oi => oi.Quantity);

                return (int)soldQuantity;
            }
            else if(activity == Activity.Inventory)
            {
                var inventoryQuantity = _dbContext.Stocks
                                                  .Where(s => s.Product.CreatedDate >= startDate && s.Product.CreatedDate <= endDate)
                                                  .Sum(s => s.UnitInStock);

                return inventoryQuantity;
            }
            else
            {
                throw new ArgumentException("Invalid activity type");
            }
        }
    }
}
