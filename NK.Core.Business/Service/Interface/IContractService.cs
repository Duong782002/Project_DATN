using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public interface IContractService
    {
        Task CreateContract(Contract contract);
        Task<IEnumerable<Contract>> GetAllContract();
    }
}
