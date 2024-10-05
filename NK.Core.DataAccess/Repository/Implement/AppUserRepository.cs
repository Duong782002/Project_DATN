using NK.Core.Model.Entities;
using NK.Core.Model.Enums;
using NK.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace NK.Core.DataAccess.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _dbContext;
        public AppUserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask Add(string userId, string userName, string password)
        {
            AppUser res = new()
            {
                UserId = userId,
                UserName = userName,
                Password = password,
                IsFirst = true,
                Status = Status.STOP
            };

            await _dbContext.AppUsers.AddAsync(res);
            _dbContext.SaveChanges();
        }

        public async ValueTask<AppUser?> GetUserLoginAsync(string userId, string password)
        {
            return await _dbContext.AppUsers.FirstOrDefaultAsync(p => p.UserId == userId && p.Password == password);
        }

        public async Task DeleteCustomer(string id)
        {
            var res = await GetUser(id);

            if(res != null)
            {
                _dbContext.AppUsers.Remove(res);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<AppUser?> GetUser(string id)
        {
            return await _dbContext.AppUsers.FindAsync(id);
        }

        public async Task<IEnumerable<AppUser>> GetAllCustomer()
        {
            return await _dbContext.AppUsers.ToListAsync();
        }

        public async Task UpdateCustomer(AppUser user)
        {
            _dbContext.AppUsers.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
