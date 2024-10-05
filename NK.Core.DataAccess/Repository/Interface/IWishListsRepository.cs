using NK.Core.Model.Entities;

namespace NK.Core.DataAccess.Repository
{
    public interface IWishListsRepository
    {
        Task<WishLists> GetItemByAppUserIDAndProductID(string appUserId, string productId, CancellationToken cancellationToken = default);
        Task<List<WishLists>> GetItemsByUserID(string appUserID, CancellationToken cancellationToken = default);
        Task AddItem(WishLists item, CancellationToken cancellationToken = default);
        void RemoveItem(string appUserId, string productId, CancellationToken cancellationToken = default);
    }
}
