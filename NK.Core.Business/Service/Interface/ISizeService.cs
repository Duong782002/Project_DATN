using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public interface ISizeService
    {

        Task<List<Size>> GetAllSizeAsync(CancellationToken cancellationToken = default);
        Task<Size> GetByIdSizeAsync(string id, CancellationToken cancellationToken = default);
        Task<Size> GetByNumberSizeAsync(int numberSize, CancellationToken cancellationToken = default);
        Task<Size> CreateAsync(Size size);
        Task<Size> UpdateByIdSize(string id, Size size, CancellationToken cancellationToken = default);
        Task<bool> DeleteSize(string id);
    }
}
