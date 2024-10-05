using NK.Core.Model.Entities;

namespace NK.Core.DataAccess.Repository
{
    public interface ISizeRepository
    {
        Task<List<Size>> GetAllSizeAsync(CancellationToken cancellationToken = default);
        Task<Size> GetByIdSizeAsync(string id, CancellationToken cancellationToken = default);
        Task<Size> GetByNumberSizeAsync(int numberSize, CancellationToken cancellationToken = default);
        Task AddSize(Size size);
        Task<bool> DeleteSize(string id);
        Task UpdateSize(Size size);
    }
}
