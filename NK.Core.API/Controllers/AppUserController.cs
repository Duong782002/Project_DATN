using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NK.Core.Business.Model.Appuser;
using NK.Core.Business.Service;
using NK.Core.Business.Utilities;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IGlobalServices _globalServices;

        public AppUserController(IAppUserService appUserService, IGlobalServices globalServices)
        {
            _appUserService = appUserService;
            _globalServices = globalServices;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(string userId, string password)
        {
            try
            {
                var res = await _appUserService.OnLoginAsync(userId, password);

                if (res != null)
                {
                    return Ok(res);
                }

                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("logincustomer")]
        public async Task<ActionResult<AppUser>> LoginCustomer([FromBody]LoginInput loginInput)
        {
            try
            {
                var res = await _appUserService.OnLoginAsync(loginInput.UserId, loginInput.password);

                if (res != null)
                {
                    return Ok(res);
                }

                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                bool res = await _appUserService.OnLogoutAsync();

                if (res)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("Có lỗi khi đăng xuất!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterInput registerInput)
        {
            bool res = await _appUserService.Register(registerInput.UserId, registerInput.UserName, registerInput.Password, registerInput.Role);

            return Ok(res);
        }

        [HttpPost("registeradmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminInput registerAdmin)
        {
            try
            {
                bool res = await _appUserService.RegisterAdmin(registerAdmin.UserId, registerAdmin.UserName, registerAdmin.Password, registerAdmin.PhoneNumber, registerAdmin.Email);

                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("changepassword/{id}")]
        public async Task<IActionResult> ChangePassword(string id, string currentPassword, string newPassword)
        {
            try
            {
                var res = await _appUserService.GetUserById(id);
                string checkPassword = StringUtil.CreateMD5(currentPassword)[..20];

                if (res == null) return NotFound();
                if(res.Password != checkPassword)
                {
                    return BadRequest(new { error = "Mật khẩu hiện tại không đúng" });
                }

                await _appUserService.ChangePassword(id, newPassword);
                return Ok("Mật khẩu đã được thay đổi thành công");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("changepasswordcustomer")]
        public async Task<IActionResult> ChangePasswordCustomer([FromBody] ChangePasswordInput changePasswordInput)
        {
            try
            {
                var res = await _appUserService.GetUserById(changePasswordInput.Id);
                string checkPassword = StringUtil.CreateMD5(changePasswordInput.CurrentPassword)[..20];

                if (res == null) return NotFound();
                if (res.Password != checkPassword)
                {
                    return BadRequest(new { error = "Mật khẩu hiện tại không đúng" });
                }

                await _appUserService.ChangePassword(changePasswordInput.Id, changePasswordInput.NewPassword);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var customer = await _appUserService.GetAllAsync();

            return Ok(customer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser?>> GetUserById(string id)
        {
            try
            {
                var res = await _appUserService.GetUserById(id);

                if (res == null) return NotFound();

                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("customer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                var customer = await _appUserService.GetAllCustomer();

                return Ok(customer);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("customerStatus")]
        public async Task<IActionResult> GetAllCustomerForStatus(Status status)
        {
            try
            {
                var customer = await _appUserService.GetCustomerForStatus(status);
                return Ok(customer);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteCustomer(string id)
        {
            await _appUserService.DeleteCustomer(id);
            return true;
        }

        [HttpPut("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(string id)
        {
            try
            {
                await _appUserService.UpdateStatus(id);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("updatecustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerInput customerInput)
        {
            try
            {
                var res = await _appUserService.GetUserById(customerInput.Id);

                if (res == null) return NotFound();

                res.UserId = customerInput.UserId;
                res.UserName = customerInput.UserName;
                res.PhoneNumber = customerInput.PhoneNumber;
                res.Email = customerInput.Email;

                await _appUserService.UpdateCustomer(res);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("checkuser")]
        public async Task<IActionResult> CheckUser(string userName, Role role)
        {
            try
            {
                var res = await _appUserService.CheckUser(userName,role);

                if (res == null) return NotFound();

                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            try
            {
                bool res = await _appUserService.ForgotPassword(forgotPassword);

                if (res)
                {
                    return NoContent();
                }

                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
