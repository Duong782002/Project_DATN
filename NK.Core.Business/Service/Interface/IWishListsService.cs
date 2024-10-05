using NK.Core.Business.Model;
using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public interface IWishListsService
    {
        Task<WishLists> GetItemByAppUserIDAndProductID(string appUserId, string productId, CancellationToken cancellationToken = default);
        Task CreateAsync(WishLists wishLists, CancellationToken cancellationToken = default);
        Task<List<WishListDto>> GetItemsByUserID(string AppUserId, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(string appUserId, string productId, CancellationToken cancellationToken = default);
        Task<bool> IsWishListExists(WishLists wish);
    }
}
