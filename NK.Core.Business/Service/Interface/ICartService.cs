using NK.Core.Business.Model.ShoppingCart;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;

namespace NK.Core.Business.Service
{
    public interface ICartService
    {
        Task<IEnumerable<ShoppingCartItems>?> GetShoppingCartItemsAsync(string userId, StatusCard status);
        Task<ShoppingCartItems?> GetShoppingCartItemsAsync(string id);
        Task<ShoppingCartItems> CreateShoppingCartItem(ShoppingPostDto cartItem);
        Task<decimal> UpdateShoppingCartItem(ShoppingCartItems cartItem);
        Task<decimal> TotalAmountShoppingCart(string UserId);
        Task<bool> DeleteCartId(string ProductId);
        Task<bool> DeleteListShoppingCart(string UserId);
    }
}
