using NK.Core.DataAccess.Repository;
using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }
        public async Task<List<Size>> GetAllSizeAsync(CancellationToken cancellationToken = default)
        {
            List<Size> sizeList = await _sizeRepository.GetAllSizeAsync(cancellationToken);
            return sizeList;
        }

        public async Task<Size> GetByIdSizeAsync(string id, CancellationToken cancellationToken = default)
        {
            Size size = await _sizeRepository.GetByIdSizeAsync(id, cancellationToken);
            return size;
        }

        public async Task<Size> GetByNumberSizeAsync(int numberSize, CancellationToken cancellationToken = default)
        {
            return await _sizeRepository.GetByNumberSizeAsync(numberSize, cancellationToken);
        }
        public async Task<Size> CreateAsync(Size size)
        {
            await _sizeRepository.AddSize(size);
            return size;
        }

        public async Task<Size> UpdateByIdSize(string id, Size size, CancellationToken cancellationToken = default)
        {
            var existingSize = await _sizeRepository.GetByIdSizeAsync(id, cancellationToken);
            if (existingSize == null)
            {
                throw new Exception("User not found.");
            }
            else
            {
                existingSize.NumberSize = size.NumberSize;
                await _sizeRepository.UpdateSize(existingSize);
                return existingSize;
            }
        }

        public async Task<bool> DeleteSize(string id)
        {
            return await _sizeRepository.DeleteSize(id);
        }
    }
}
