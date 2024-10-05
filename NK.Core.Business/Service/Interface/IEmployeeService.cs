using NK.Core.Business.Model.Employee;
using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public interface IEmployeeService
    {
        Task<IEnumerable<AppUser>> GetAllEmployee();
        Task<bool> CreateEmployee(EmployeeCreate employee);
        Task<bool> UpdateEmployee(EmployeeCreate employee);
        Task<bool> DeleteEmployee(string id);
        Task<AppUser?> GetEmployeeId(string id);

        //doanh so tong
        Task<decimal> GetTotalAmount(string id);
        Task<decimal> GetTotalBill(string id);

        //doanh thu của tháng hiện tại
        Task<decimal> GetTotalAmountForCurrentMonth(string id);
        Task<decimal> GetTotalBillForCurrentMonth(string id);

        //doanh thu của hôm nay
        Task<decimal> GetTotalAmountForToday(string id);
        Task<decimal> GetTotalBillForToday(string id);

        //doanh thu trong khoảng thời gian
        Task<decimal> GetTotalAmountForTime(DateTime startDate, DateTime endDate, string id);
        Task<decimal> GetTotalBillForTime(DateTime startDate, DateTime endDate,string id);
    }
}
