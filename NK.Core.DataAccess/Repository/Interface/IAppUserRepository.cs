using NK.Core.Model.Entities;
using NK.Core.Model.Enums;

namespace NK.Core.DataAccess.Repository
{
    public interface IAppUserRepository
    {
        ValueTask<AppUser?> GetUserLoginAsync(string userId, string password);
        ValueTask Add(string userId, string userName, string password);
        Task DeleteCustomer(string id);
        Task<AppUser?> GetUser(string id);
        Task<IEnumerable<AppUser>> GetAllCustomer();
        Task UpdateCustomer(AppUser user);
    }
}
