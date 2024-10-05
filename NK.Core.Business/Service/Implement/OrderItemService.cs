using Microsoft.EntityFrameworkCore;
using NK.Core.Business.Service.Interface;
using NK.Core.Model;
using NK.Core.Model.Enums;

namespace NK.Core.Business.Service.Implement
{
    public class OrderItemService : IOrderItemService
    {
        private readonly AppDbContext _dbContext;
        public OrderItemService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<decimal> GetTotalAmount()
        {
            var totalAmount = await _dbContext.Orders
                .Where(p => p.CurrentStatus == StatusOrder.DELIVERIED)
                .Select(p => p.TotalAmount)
                .SumAsync();

            return totalAmount;
        }

        public async Task<decimal> GetTotalAmountForCurrentMonth()
        {
            DateTime today = DateTime.Today;
            DateTime startDate = new DateTime(today.Year, today.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            var orders = await _dbContext.Orders
                .Where(p => p.DateCreated >= startDate && p.DateCreated <= endDate && p.CurrentStatus == StatusOrder.DELIVERIED)
                .ToListAsync();

            decimal totalAmount = orders.Select(p => p.TotalAmount).Sum();

            return totalAmount;
        }

        public async Task<decimal> GetTotalAmountForTime(DateTime startDate, DateTime endDate)
        {
            var orders = await _dbContext.Orders
                .Where(p => p.DateCreated >= startDate && p.DateCreated <= endDate && p.CurrentStatus == StatusOrder.DELIVERIED)
                .ToListAsync();

            decimal totalAmount = orders.Select(p => p.TotalAmount).Sum();

            return totalAmount;
        }

        public async Task<decimal> GetTotalAmountForToday()
        {
            var orders = await _dbContext.Orders
                .Where(p => p.DateCreated.Date == DateTime.Now.Date && p.CurrentStatus == StatusOrder.DELIVERIED)
                .ToListAsync();

            decimal totalAmount = orders.Select(p => p.TotalAmount).Sum();

            return totalAmount;
        }

        public async Task<decimal> GetTotalBill()
        {
            var totalOrders = await _dbContext.Orders
                .Where(p => p.CurrentStatus == StatusOrder.DELIVERIED)
                .CountAsync();

            return totalOrders;
        }

        public async Task<decimal> GetTotalBillForCurrentMonth()
        {
            DateTime today = DateTime.Today;
            DateTime startDate = new DateTime(today.Year, today.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            var totalOrders = await _dbContext.Orders
                .Where(p => p.DateCreated >= startDate && p.DateCreated <= endDate && p.CurrentStatus == StatusOrder.DELIVERIED)
                .CountAsync();

            return totalOrders;
        }

        public async Task<decimal> GetTotalBillForTime(DateTime startDate, DateTime endDate)
        {
            var totalOrders = await _dbContext.Orders
                .Where(p => p.DateCreated >= startDate && p.DateCreated <= endDate && p.CurrentStatus == StatusOrder.DELIVERIED)
                .CountAsync();

            return totalOrders;
        }

        public async Task<decimal> GetTotalBillForToday()
        {
            var totalOrders = await _dbContext.Orders
                .Where(p => p.DateCreated.Date == DateTime.Now.Date && p.CurrentStatus == StatusOrder.DELIVERIED)
                .CountAsync();

            return totalOrders;
        }
    }
}
