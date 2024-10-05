using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NK.Core.Business.Model.Appuser;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;

namespace NK.Core.Business.Service
{
    public interface IAppUserService
    {
        ValueTask<bool> Register(string userId, string userName, string password, Role role);
        ValueTask<bool> RegisterAdmin(string userId, string userName, string password, string phoneNumber, string Email);
        ValueTask<AppUser?> OnLoginAsync(string userId, string password);
        ValueTask<bool> OnLogoutAsync();
        Task<IEnumerable<AppUser>> GetAllAsync();
        Task<IEnumerable<AppUser>> GetAllCustomer();
        Task UpdateStatus(string id);
        Task<IEnumerable<AppUser>?> GetCustomerForStatus(Status status);
        Task<AppUser?> GetUserById(string id);
        Task ChangePassword(string id, string newPassword);
        Task DeleteCustomer(string id);
        Task UpdateCustomer(AppUser user);
        Task<AppUser?> GetUser(string id);
        Task<AppUser?> CheckUser(string userName, Role role);
        Task<bool> ForgotPassword(ForgotPassword forgotPassword);
    }
}
