using NK.Core.Business.Model;
using NK.Core.DataAccess.Repository;
using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public class WishListsService : IWishListsService
    {
        private readonly IWishListsRepository _wishListsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WishListsService(IWishListsRepository wishListsRepository, IUnitOfWork unitOfWork)
        {
            _wishListsRepository = wishListsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(WishLists wishLists, CancellationToken cancellationToken = default)
        {

            await _wishListsRepository.AddItem(wishLists);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> IsWishListExists(WishLists wish)
        {
            var wishListExsist = await _wishListsRepository.GetItemByAppUserIDAndProductID(wish.UserId, wish.ProductsId);
            if (wishListExsist == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAsync(string appUserId, string productId, CancellationToken cancellationToken = default)
        {
            var item = await _wishListsRepository.GetItemByAppUserIDAndProductID(appUserId, productId, cancellationToken);
            if (item != null)
            {
                _wishListsRepository.RemoveItem(appUserId, productId);
                await _unitOfWork.SaveChangeAsync();

                return true;
            }
            return false;
        }

        public async Task<List<WishListDto>> GetItemsByUserID(string AppUserId, CancellationToken cancellationToken = default)
        {
            var items = await _wishListsRepository.GetItemsByUserID(AppUserId);
            var wishlistDto = items.Select(i => new WishListDto
            {
                Id = i.Product.Id,
                Name = i.Product.Name,
                DiscountRate = i.Product.DiscountRate,
                RetailPrice = i.Product.RetailPrice
            }).ToList();
            return wishlistDto;
        }
        public async Task<WishLists> GetItemByAppUserIDAndProductID(string appUserId, string productId, CancellationToken cancellationToken = default)
        {
            var item = _wishListsRepository.GetItemByAppUserIDAndProductID(appUserId, productId, cancellationToken);
            return await item;
        }
    }
}
