using Microsoft.AspNetCore.Mvc;
using NK.Core.Business.Model;
using NK.Core.Business.Model.ShoppingCart;
using NK.Core.Business.Service;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IStockService _stockService;
        private readonly ISizeService _sizeService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IStockService stockService, ISizeService sizeService, IProductService productService)
        {
            _cartService = cartService;
            _stockService = stockService;
            _sizeService = sizeService;
            _productService = productService;
        }

        [HttpGet("userId")]
        public async Task<ActionResult<IEnumerable<ShoppingCartDto>>> GetShoppingCartItem(string userId, StatusCard status)
        {
            try
            {
                var listShoppingCartDto = new List<ShoppingCartDto>();
                var listShoppingCart = await _cartService.GetShoppingCartItemsAsync(userId, status);

                if (listShoppingCart == null) return NotFound();

                foreach(var shoppingCart in listShoppingCart)
                {
                    var stocks = await _stockService.GetStockByIdAsync(shoppingCart.ProductId);
                    var product = await _productService.GetProductById(shoppingCart.ProductId);

                    var stocksDto = new List<StockDto>();
                    foreach (var stock in stocks)
                    {
                        var numberSize = await _sizeService.GetByIdSizeAsync(stock.SizeId);

                        var stockDto = new StockDto()
                        {
                            Id = stock.SizeId,
                            NumberSize = numberSize.NumberSize,
                            UnitInStock = stock.UnitInStock
                        };

                        stocksDto.Add(stockDto);
                    }

                    var sortedStocksDto = stocksDto.OrderBy(s => s.NumberSize).ToList();

                    var shoppingCartDto = new ShoppingCartDto()
                    {
                        Id = shoppingCart.Id,
                        UserId = shoppingCart.UserId,
                        ProductId = shoppingCart.ProductId,
                        Name = product?.Name ?? string.Empty,
                        Image = Url.Action(nameof(GetProductImage), new { id = shoppingCart.ProductId }) ?? string.Empty,
                        DiscountRate = product?.DiscountRate ?? decimal.Zero,
                        Stocks = sortedStocksDto
                    };

                    listShoppingCartDto.Add(shoppingCartDto);
                }

                return Ok(listShoppingCartDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("useridafterupdate")]
        public async Task<ActionResult<IEnumerable<ShoppingCartViewId>>> GetShoppingCartAfterUpdate(string userId, StatusCard status)
        {
            try
            {
                var listShoppingCartViewDto = new List<ShoppingCartViewId>();
                var listShoppingCartView = await _cartService.GetShoppingCartItemsAsync(userId, status);

                if (listShoppingCartView == null) return NotFound();

                foreach(var shoppingCart in listShoppingCartView)
                {
                    var product = await _productService.GetProductById(shoppingCart.ProductId);
                    var size = await _sizeService.GetByIdSizeAsync(shoppingCart?.SizeId ?? string.Empty);

                    var result = new ShoppingCartViewId()
                    {
                        Id = shoppingCart?.Id ?? string.Empty,
                        ProductName = product?.Name ?? string.Empty,
                        Image = Url.Action(nameof(GetProductImage), new { id = shoppingCart?.ProductId }) ?? string.Empty,
                        NumberSize = size.NumberSize,
                        Quantity = shoppingCart?.Quantity ?? int.MinValue,
                        DiscountRate = product?.DiscountRate ?? decimal.Zero,
                        Price = shoppingCart?.Quantity * product?.DiscountRate ?? decimal.Zero
                    };

                    listShoppingCartViewDto.Add(result);
                }

                return Ok(listShoppingCartViewDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<ShoppingCartViewId>> GetShoppingCartViewId(string id)
        {
            try
            {
                var res = await _cartService.GetShoppingCartItemsAsync(id);
                var product = await _productService.GetProductById(res?.ProductId ?? string.Empty);
                var size = await _sizeService.GetByIdSizeAsync(res?.SizeId ?? string.Empty);

                var result = new ShoppingCartViewId()
                {
                    Id = res?.Id ?? string.Empty,
                    ProductName = product?.Name ?? string.Empty,
                    Image = Url.Action(nameof(GetProductImage), new { id = res?.ProductId }) ?? string.Empty,
                    NumberSize = size.NumberSize,
                    Quantity = res.Quantity,
                    DiscountRate = product?.DiscountRate ?? decimal.Zero,
                    Price = res?.Quantity * product?.DiscountRate ?? decimal.Zero
                };

                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCartItems>> CreateShoppingCartItem([FromBody] ShoppingPostDto input)
        {
            try
            {

                var res = await _cartService.CreateShoppingCartItem(input);

                if (res == null)
                {
                    return NotFound();
                }
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShoppingCartItem([FromBody] ShoppingUpdateDto input)
        {
            try
            {
                var shoppingCart = new ShoppingCartItems()
                {
                    Id = input.Id,
                    UserId = input.UserId,
                    ProductId = input.ProductId,
                    Quantity = input.Quantity,
                    SizeId = input.SizeId
                };

                var res = await _cartService.UpdateShoppingCartItem(shoppingCart);

                if (res == decimal.Zero)
                {
                    return NotFound();
                }
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("product")]
        public async Task<IActionResult> DeleteProductForCart(string id)
        {
            try
            {
                var res =  await _cartService.DeleteCartId(id);

                if (!res)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("user")]
        public async Task<IActionResult> DeleteListCart(string userId)
        {
            try
            {
                var res = await _cartService.DeleteListShoppingCart(userId);

                if (!res)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("totalamount")]
        public async Task<ActionResult<decimal>> TotalAmountShoppingCart(string userId)
        {
            try
            {
                var res = await _cartService.TotalAmountShoppingCart(userId);

                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: api/products/1/image
        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetProductImage(string id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null || product.ProductImage == null)
            {
                return NotFound();
            }

            return File(product.ProductImage, "image/jpeg");
        }
    }
}
