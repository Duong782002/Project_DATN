using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NK.Core.Business.Model.Employee;
using NK.Core.Business.Service;
using NK.Core.Model;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly AppDbContext _dbContext;
        public EmployeeController(IEmployeeService employeeService, AppDbContext dbContext)
        {
            _employeeService = employeeService;
            _dbContext = dbContext;
        }

        [HttpGet("getallemployee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var res = await _employeeService.GetAllEmployee();

                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("createemployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreate employee)
        {
            try
            {
                var res = await _employeeService.CreateEmployee(employee);

                if (res) return NoContent();

                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeCreate employee)
        {
            try
            {
                var res = await _employeeService.UpdateEmployee(employee);

                if (res) return NoContent();

                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {
                var res = await _employeeService.DeleteEmployee(id);

                if (res) return NoContent();

                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            try
            {
                var res = await _employeeService.GetEmployeeId(id);

                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("gettotalamountbymonth")]
        public async Task<IActionResult> GetTotalAmountByMonth(string id)
        {
            try
            {
                // Get the current year
                int currentYear = DateTime.Now.Year;

                // Generate a list of all months in a year (1 through 12)
                var allMonths = Enumerable.Range(1, 6)
                                          .Select(m => new { Month = m, TotalAmount = 0m })
                                          .ToList();

                // Get the total amount grouped by month for the current year and user
                var result = await _dbContext.Orders
                                             .Where(p => p.DateCreated.Year == currentYear && p.UserId == id)
                                             .GroupBy(p => p.DateCreated.Month)
                                             .Select(g => new
                                             {
                                                 Month = g.Key,
                                                 TotalAmount = g.Sum(p => p.TotalAmount),
                                                 TotalBill = g.Count()
                                             })
                                             .ToListAsync();

                // Merge the results with all months to ensure all months are included
                var finalResult = allMonths
                                  .GroupJoin(result,
                                             m => m.Month,
                                             r => r.Month,
                                             (m, r) => new { m.Month, TotalAmount = r.FirstOrDefault()?.TotalAmount ?? 0m, TotalBill = r.FirstOrDefault()?.TotalBill ?? 0m })
                                  .OrderBy(p => p.Month)
                                  .ToList();

                return Ok(finalResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("totalAmountAll")]
        public async Task<IActionResult> GetTotalAmount(string id)
        {
            try
            {
                decimal totalAmount = await _employeeService.GetTotalAmount(id);
                return Ok(totalAmount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("totalBillAll")]
        public async Task<IActionResult> GetTotalBill(string id)
        {
            try
            {
                decimal totalAmount = await _employeeService.GetTotalBill(id);
                return Ok(totalAmount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("totalAmountMonth")]
        public async Task<IActionResult> GetTotalAmountForMonth(string id)
        {
            try
            {
                decimal totalAmount = await _employeeService.GetTotalAmountForCurrentMonth(id);
                return Ok(totalAmount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("totalBillMonth")]
        public async Task<IActionResult> GetTotalBillForMonth(string id)
        {
            try
            {
                decimal totalAmount = await _employeeService.GetTotalBillForCurrentMonth(id);
                return Ok(totalAmount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("totalAmountToday")]
        public async Task<IActionResult> GetTotalAmountForToday(string id)
        {
            try
            {
                decimal totalAmount = await _employeeService.GetTotalAmountForToday(id);
                return Ok(totalAmount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("totalBillToday")]
        public async Task<IActionResult> GetTotalBillForToday(string id)
        {
            try
            {
                decimal totalAmount = await _employeeService.GetTotalBillForToday(id);
                return Ok(totalAmount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("getname")]
        public async Task<IActionResult> GetUserName(string id)
        {
            try
            {
                var res = await _dbContext.AppUsers.FindAsync(id);

                if (res == null) return NotFound();

                return Ok(res.UserName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
