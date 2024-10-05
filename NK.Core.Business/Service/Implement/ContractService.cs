using Microsoft.EntityFrameworkCore;
using NK.Core.Model;
using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public class ContractService : IContractService
    {
        private readonly AppDbContext _dbContext;

        public ContractService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateContract(Contract contract)
        {
            await _dbContext.Contracts.AddAsync(contract);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contract>> GetAllContract()
        {
            return await _dbContext.Contracts.ToListAsync();
        }
    }
}
