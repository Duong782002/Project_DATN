using NK.Core.Business.Utilities;
using NK.Core.DataAccess.Repository;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;
using NK.Core.Model;
using Microsoft.EntityFrameworkCore;
using NK.Core.Business.Model.Appuser;
using System.Data;

namespace NK.Core.Business.Service
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IGlobalServices _globalServices;
        private readonly AppDbContext _dbContext;
        public AppUserService(IAppUserRepository appUserRepository, IGlobalServices globalServices, AppDbContext dbContext)
        {
            _appUserRepository = appUserRepository;
            _globalServices = globalServices;
            _dbContext = dbContext;
        }

        public async ValueTask<AppUser?> OnLoginAsync(string userId, string password)
        {
            string checkPassword = StringUtil.CreateMD5(password)[..20];
            AppUser? res = await _appUserRepository.GetUserLoginAsync(userId, checkPassword);

            if (res != null)
            {
                if(res.Roles == Role.Customer)
                {
                    res.Status = Status.ACTIVE;
                    _globalServices.CurrentUser = res;
                    _globalServices.IsFirst = res.IsFirst;

                    if (res.IsFirst)
                    {
                        res.IsFirst = false;
                    }

                    _dbContext.AppUsers.Update(res);
                    _dbContext.SaveChanges();
                }
            }

            return res;
        }

        public async ValueTask<bool> OnLogoutAsync()
        {
            if (_globalServices.CurrentUser != null)
            {
                AppUser? res = await _appUserRepository.GetUserLoginAsync(_globalServices.CurrentUser.UserId, _globalServices.CurrentUser.Password);
                if (res != null)
                {
                    res.Status = Status.STOP;
                    _dbContext.AppUsers.Update(res);
                    _dbContext.SaveChanges();
                }
                _globalServices.CurrentUser = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        public async ValueTask<bool> Register(string userId, string userName, string password, Role role)
        {
            string checkPassword = StringUtil.CreateMD5(password)[..20];
            AppUser? res = await _appUserRepository.GetUserLoginAsync(userId, checkPassword);

            if (res == null)
            {
                await _appUserRepository.Add(userId, userName, checkPassword);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task DeleteCustomer(string id)
        {
            await _appUserRepository.DeleteCustomer(id);
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _appUserRepository.GetAllCustomer();
        }

        public async Task UpdateCustomer(AppUser user)
        {
            await _appUserRepository.UpdateCustomer(user);
        }

        public async Task<AppUser?> GetUserById(string id)
        {
            return await _appUserRepository.GetUser(id);
        }

        public async Task<IEnumerable<AppUser>> GetAllCustomer()
        {
            return await _dbContext.AppUsers.Where(p => p.Roles == Role.Customer).ToListAsync();
        }

        public async Task UpdateStatus(string id)
        {
            var user = await GetUserById(id);

            if(user != null)
            {
                user.Status = user.Status == Status.BLOCK ? Status.STOP : Status.BLOCK;
                await _appUserRepository.UpdateCustomer(user);
            }
        }

        public async Task<IEnumerable<AppUser>?> GetCustomerForStatus(Status status)
        {
            return await _dbContext.AppUsers.Where(p => p.Roles == Role.Customer && p.Status == status).ToListAsync();
        }

        public async Task<AppUser?> GetUser(string id)
        {
            return await _appUserRepository.GetUser(id);
        }

        public async Task ChangePassword(string id, string newPassword)
        {
            var res = await GetUserById(id);
            string password = StringUtil.CreateMD5(newPassword)[..20];

            if (res != null)
            {
                res.Password = password;
                _dbContext.AppUsers.Update(res);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<AppUser?> CheckUser(string userName, Role role)
        {
            var res = await _dbContext.AppUsers.Where(p => p.UserId.ToLower() == userName.ToLower() && p.Roles == role).FirstAsync();

            return res;
        }

        public async Task<bool> ForgotPassword(ForgotPassword forgotPassword)
        {
            var res = await _dbContext.AppUsers.Where(p => p.UserId.ToLower() == forgotPassword.UserName.ToLower() && p.Roles == forgotPassword.Role).FirstAsync();
            string password = StringUtil.CreateMD5(forgotPassword.Password)[..20];

            if (res != null)
            {
                res.Password = password;
                _dbContext.AppUsers.Update(res);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async ValueTask<bool> RegisterAdmin(string userId, string userName, string password, string phoneNumber, string Email)
        {
            string checkPassword = StringUtil.CreateMD5(password)[..20];
            AppUser? res = await _appUserRepository.GetUserLoginAsync(userId, checkPassword);

            if (res == null)
            {
                var user = new AppUser()
                {
                    UserId = userId,
                    UserName = userName,
                    Password = checkPassword,
                    PhoneNumber = phoneNumber,
                    Email = Email,
                    Roles = Role.Admin
                };

                await _dbContext.AppUsers.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
