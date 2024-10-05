using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NK.Core.Business.Model;
using NK.Core.Business.Service;
using NK.Core.Model.Entities;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListsService _wishListsService;
        private readonly IProductService _productService;
        public WishListController(IWishListsService wishListsService, IProductService productService)
        {
            _wishListsService = wishListsService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWishListByUser(string userId)
        {
            try
            {
                var wishlistDto = await _wishListsService.GetItemsByUserID(userId);

                if (wishlistDto.Count == 0) return NotFound();
                var listWishLish = new List<WishListDto>();

                foreach(var wishlist in wishlistDto)
                {
                    var resetWishListDto = new WishListDto()
                    {
                        Id = wishlist.Id,
                        Name = wishlist.Name,
                        ImgUrl = Url.Action(nameof(GetProductImage), new { id = wishlist.Id }) ?? string.Empty,
                        RetailPrice = wishlist.RetailPrice,
                        DiscountRate = wishlist.DiscountRate
                    };

                    listWishLish.Add(resetWishListDto);
                }

                return Ok(listWishLish);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWishList([FromBody] WishListInputDto input)
        {
            try
            {
                var wishlist = new WishLists()
                {
                    UserId = input.UserId,
                    ProductsId = input.ProductId
                };

                await _wishListsService.CreateAsync(wishlist);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteWishList([FromBody] WishListInputDto input)
        {
            try
            {
                var res = await _wishListsService.DeleteAsync(input.UserId, input.ProductId);

                return res;
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
