using Microsoft.EntityFrameworkCore;
using NK.Core.Business.Model.ShoppingCart;
using NK.Core.Model;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;

namespace NK.Core.Business.Service
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _dbContext;

        public CartService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ShoppingCartItems>?> GetShoppingCartItemsAsync(string userId, StatusCard status)
        {
            var query = await _dbContext.ShoppingCartItems
                                .Where(p => p.UserId == userId && p.Status == status)
                                .ToListAsync();

            return query;
        }
        public async Task<ShoppingCartItems?> GetShoppingCartItemsAsync(string id)
        {
            return await _dbContext.ShoppingCartItems.FindAsync(id);
        }

        public async Task<ShoppingCartItems> CreateShoppingCartItem(ShoppingPostDto cartItem)
        {
            if(cartItem.Status == StatusCard.Product)
            {
                var res = await _dbContext.ShoppingCartItems.Where(p => p.UserId == cartItem.UserId && p.ProductId == cartItem.ProductId && p.Status == StatusCard.Product).FirstOrDefaultAsync();

                if(res != null)
                {
                    res.SizeId = cartItem.SizeId;
                    res.Quantity = cartItem.Quantity;

                    _dbContext.ShoppingCartItems.Update(res);
                    await _dbContext.SaveChangesAsync();
                    return res;
                }
                else
                {
                    var cart = new ShoppingCartItems()
                    {
                        UserId = cartItem.UserId,
                        ProductId = cartItem.ProductId,
                        SizeId = cartItem.SizeId,
                        Quantity = cartItem.Quantity,
                        Status = StatusCard.Product,
                    };

                    await _dbContext.ShoppingCartItems.AddAsync(cart);
                    await _dbContext.SaveChangesAsync();
                    return cart;
                }
            }
            else
            {
                var cart = new ShoppingCartItems()
                {
                    UserId = cartItem.UserId,
                    ProductId = cartItem.ProductId,
                    SizeId = string.Empty,
                    Status = StatusCard.Cart,
                };

                await _dbContext.ShoppingCartItems.AddAsync(cart);
                await _dbContext.SaveChangesAsync();
                return cart;
            }
        }

        public async Task<decimal> UpdateShoppingCartItem(ShoppingCartItems cartItem)
        {
            var res = await _dbContext.ShoppingCartItems.Where(p => p.Id == cartItem.Id).FirstOrDefaultAsync();

            if(res != null)
            {
                var discountRate = await _dbContext.Products.Where(p => p.Id == res.ProductId).FirstOrDefaultAsync();

                res.SizeId = cartItem.SizeId;
                res.Quantity = cartItem.Quantity;
                res.Price = cartItem.Quantity * discountRate.DiscountRate;

                _dbContext.ShoppingCartItems.Update(res);
                await _dbContext.SaveChangesAsync();

                return res.Price;
            }
            return decimal.Zero;
        }

        public async Task<bool> DeleteCartId(string id)
        {
            var res = await _dbContext.ShoppingCartItems.Where(p => p.Id == id && p.Status == StatusCard.Cart).FirstOrDefaultAsync();

            if(res != null)
            {
                _dbContext.ShoppingCartItems.Remove(res);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> DeleteListShoppingCart(string userId)
        {
            var res = await _dbContext.ShoppingCartItems.Where(p => p.UserId == userId && p.Status == StatusCard.Cart).ToListAsync();

            if (res.Any())
            {
                _dbContext.ShoppingCartItems.RemoveRange(res);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<decimal> TotalAmountShoppingCart(string userId)
        {
            var res = await _dbContext.ShoppingCartItems.Where(p => p.UserId == userId).ToListAsync();

            if (res.Any())
            {
                var totalAmount = res.Sum(p => p.Price);

                return totalAmount;
            }
            return decimal.Zero;
        }

    }
}
