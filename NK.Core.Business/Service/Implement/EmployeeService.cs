using Microsoft.EntityFrameworkCore;
using NK.Core.Business.Model.Employee;
using NK.Core.Business.Utilities;
using NK.Core.Model;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;

namespace NK.Core.Business.Service.Implement
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _dbContext;

        public EmployeeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateEmployee(EmployeeCreate employee)
        {
            string password = "1234";
            string checkPassword = StringUtil.CreateMD5(password)[..20];

            var res = new AppUser()
            {
                UserId = employee.UserId,
                UserName = employee.UserName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Password = checkPassword,
                AddressName = employee.AddressName,
                Gender = employee.Gender,
                Status = Status.ACTIVE,
                Roles = Role.Employee,
                ModifiedDate = employee.DateOfBirth
            };

           if(res != null)
            {
                await _dbContext.AppUsers.AddAsync(res);
                await _dbContext.SaveChangesAsync();

                return true;
            }

           return false;
        }

        public async Task<IEnumerable<AppUser>> GetAllEmployee()
        {
            return await _dbContext.AppUsers.Where(p => p.Roles == Role.Employee).ToListAsync();
        }

        public async Task<bool> UpdateEmployee(EmployeeCreate employee)
        {
            var res = await GetEmployeeId(employee.Id);

            if(res != null)
            {
                res.UserName = employee.UserName;
                res.Email = employee.Email;
                res.PhoneNumber = employee.PhoneNumber;
                res.AddressName = employee.AddressName;
                res.Gender = employee.Gender;
                res.ModifiedDate = employee.DateOfBirth;
                res.Status = employee.Status;

                _dbContext.AppUsers.Update(res);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<AppUser?> GetEmployeeId(string id)
        {
            return await _dbContext.AppUsers.Where(p => p.Id == id && p.Roles == Role.Employee).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteEmployee(string id)
        {
            var res = await _dbContext.AppUsers.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (res != null)
            {
                _dbContext.AppUsers.Remove(res);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<decimal> GetTotalAmount(string id)
        {
            var totalAmount = await _dbContext.Orders
                        .Where(p => p.CurrentStatus == StatusOrder.DELIVERIED && p.UserId == id)
                        .Select(p => p.TotalAmount)
                        .SumAsync();

            return totalAmount;
        }

        public async Task<decimal> GetTotalBill(string id)
        {
            var totalOrders = await _dbContext.Orders
                        .Where(p => p.CurrentStatus == StatusOrder.DELIVERIED && p.UserId == id)
                        .CountAsync();

            return totalOrders;
        }

        public async Task<decimal> GetTotalAmountForCurrentMonth(string id)
        {
            DateTime today = DateTime.Today;
            DateTime startDate = new DateTime(today.Year, today.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            var orders = await _dbContext.Orders
                    .Where(p => p.DateCreated >= startDate && p.DateCreated <= endDate && p.CurrentStatus == StatusOrder.DELIVERIED && p.UserId == id)
                    .ToListAsync();

            decimal totalAmount = orders.Select(p => p.TotalAmount).Sum();

            return totalAmount;
        }

        public async Task<decimal> GetTotalBillForCurrentMonth(string id)
        {
            DateTime today = DateTime.Today;
            DateTime startDate = new DateTime(today.Year, today.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            var totalOrders = await _dbContext.Orders
                        .Where(p => p.DateCreated >= startDate && p.DateCreated <= endDate && p.CurrentStatus == StatusOrder.DELIVERIED && p.UserId == id)
                        .CountAsync();

            return totalOrders;
        }

        public async Task<decimal> GetTotalAmountForToday(string id)
        {
            var orders = await _dbContext.Orders
                    .Where(p => p.DateCreated.Date == DateTime.Now.Date && p.CurrentStatus == StatusOrder.DELIVERIED && p.UserId == id)
                    .ToListAsync();

            decimal totalAmount = orders.Select(p => p.TotalAmount).Sum();

            return totalAmount;
        }

        public async Task<decimal> GetTotalBillForToday(string id)
        {
            var totalOrders = await _dbContext.Orders
                        .Where(p => p.DateCreated.Date == DateTime.Now.Date && p.CurrentStatus == StatusOrder.DELIVERIED && p.UserId == id)
                        .CountAsync();

            return totalOrders;
        }

        public async Task<decimal> GetTotalAmountForTime(DateTime startDate, DateTime endDate, string id)
        {
            var orders = await _dbContext.Orders
                    .Where(p => p.DateCreated >= startDate && p.DateCreated <= endDate && p.CurrentStatus == StatusOrder.DELIVERIED && p.UserId == id)
                    .ToListAsync();

            decimal totalAmount = orders.Select(p => p.TotalAmount).Sum();

            return totalAmount;
        }

        public async Task<decimal> GetTotalBillForTime(DateTime startDate, DateTime endDate, string id)
        {
            var totalOrders = await _dbContext.Orders
                        .Where(p => p.DateCreated >= startDate && p.DateCreated <= endDate && p.CurrentStatus == StatusOrder.DELIVERIED && p.UserId == id)
                        .CountAsync();

            return totalOrders;
        }
    }
}
