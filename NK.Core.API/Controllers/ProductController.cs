using Microsoft.AspNetCore.Mvc;
using NK.Core.Business.Model;
using NK.Core.Business.Model.Product;
using NK.Core.Business.Model.Products;
using NK.Core.Business.Service;
using NK.Core.Model;
using NK.Core.Model.Entities;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStockService _stockService;
        private readonly ISizeService _sizeService;
        private readonly IWishListsService _wishListsService;
        private readonly IGlobalServices _globalServices;
        private readonly AppDbContext _dbContext;

        public ProductController(IProductService productService, IStockService stockService, ISizeService sizeService, IWishListsService wishListsService, IGlobalServices globalServices, AppDbContext dbContext)
        {
            _productService = productService;
            _stockService = stockService;
            _sizeService = sizeService;
            _wishListsService = wishListsService;
            _globalServices = globalServices;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductView>>> GetProducts()
        {

            var products = await _productService.GetAllProduct();

            if (products == null)
            {
                return NotFound();
            }
            var productViews = new List<ProductView>();

            foreach (var product in products)
            {
                var totalQuantity = await _stockService.TotalQuantityProduct(product.Id);

                var productView = new ProductView()
                {
                    Id = product.Id,
                    Name = product.Name ?? string.Empty,
                    Image = Url.Action(nameof(GetProductImage), new { id = product.Id }) ?? string.Empty,
                    RetailPrice = product.RetailPrice,
                    DiscountRate = product.DiscountRate,
                    TaxRate = product.TaxRate ?? decimal.Zero,
                    DiscountType = product.DiscountType,
                    TotalQuantity = totalQuantity,
                    Status = product.Status
                };

                productViews.Add(productView);
            }

            return Ok(productViews);
        }

        [HttpGet("pageproduct")]
        public async Task<ActionResult<IEnumerable<ProductView>>> Test(int page = 1, int pageSize = 9)
        {
            // Tính chỉ số bắt đầu và chỉ số kết thúc của sản phẩm trên trang hiện tại
            int startIndex = (page - 1) * pageSize;
            int endIndex = startIndex + pageSize;

            var products = await _productService.GetAllProduct();

            if (products == null)
            {
                return NotFound();
            }

            var totalPages = (int)Math.Ceiling((double)products.Count() / pageSize);
            // Lấy một phần của danh sách sản phẩm tương ứng với trang hiện tại
            var pagedProducts = products.Skip(startIndex).Take(pageSize);

            var productViews = new List<ProductView>();

            foreach (var product in pagedProducts)
            {
                var totalQuantity = await _stockService.TotalQuantityProduct(product.Id);

                var wishList = new WishLists()
                {
                    UserId = _globalServices.CurrentUser?.Id ?? string.Empty,
                    ProductsId = product.Id
                };

                var heart = await _wishListsService.IsWishListExists(wishList);

                var productView = new ProductView()
                {
                    Id = product.Id,
                    Name = product.Name ?? string.Empty,
                    Image = Url.Action(nameof(GetProductImage), new { id = product.Id }) ?? string.Empty,
                    RetailPrice = product.RetailPrice,
                    DiscountRate = product.DiscountRate,
                    TaxRate = product.TaxRate ?? decimal.Zero,
                    DiscountType = product.DiscountType,
                    TotalQuantity = totalQuantity,
                    Status = product.Status,
                    Heart = heart
                };

                productViews.Add(productView);
            }

            return Ok(new { Products = productViews, TotalPages = totalPages });
        }

        [HttpGet("productdeal")]
        public async Task<ActionResult<IEnumerable<ProductView>>> GetProductCount()
        {
            var products = await _productService.GetProductCount(8);

            if (products == null)
            {
                return NotFound();
            }
            var productViews = new List<ProductView>();

            foreach (var product in products)
            {
                var totalQuantity = await _stockService.TotalQuantityProduct(product.Id);

                var productView = new ProductView()
                {
                    Id = product.Id,
                    Name = product.Name ?? string.Empty,
                    Image = Url.Action(nameof(GetProductImage), new { id = product.Id }) ?? string.Empty,
                    RetailPrice = product.RetailPrice,
                    DiscountRate = product.DiscountRate,
                    TaxRate = product.TaxRate ?? decimal.Zero,
                    DiscountType = product.DiscountType,
                    TotalQuantity = totalQuantity,
                    Status = product.Status
                };

                productViews.Add(productView);
            }

            return Ok(productViews);
        }

        [HttpGet("filtercustomer")]
        public async Task<ActionResult<IEnumerable<ProductView>>> Filter([FromQuery] ProductFilterOptionAPI filter)
        {
            int startIndex = (filter.Page - 1) * filter.PageSize;
            int endIndex = startIndex + filter.PageSize;

            var products = await _productService.FilterProductForCustomer(filter);

            if (products == null || products?.Count == 0)
            {
                return NotFound();
            }

            var totalPages = (int)Math.Ceiling((double)products.Count() / filter.PageSize);
            // Lấy một phần của danh sách sản phẩm tương ứng với trang hiện tại
            var pagedProducts = products.Skip(startIndex).Take(filter.PageSize);

            var productViews = new List<ProductView>();

            foreach (var product in pagedProducts)
            {
                var totalQuantity = await _stockService.TotalQuantityProduct(product.Id);

                var wishList = new WishLists()
                {
                    UserId = _globalServices.CurrentUser?.Id ?? string.Empty,
                    ProductsId = product.Id
                };

                var heart = await _wishListsService.IsWishListExists(wishList);

                var productView = new ProductView()
                {
                    Id = product.Id,
                    Name = product.Name ?? string.Empty,
                    Image = Url.Action(nameof(GetProductImage), new { id = product.Id }) ?? string.Empty,
                    RetailPrice = product.RetailPrice,
                    DiscountRate = product.DiscountRate,
                    TaxRate = product.TaxRate ?? decimal.Zero,
                    DiscountType = product.DiscountType,
                    TotalQuantity = totalQuantity,
                    Status = product.Status,
                    Heart = heart
                };

                productViews.Add(productView);
            }

            return Ok(new { Products = productViews, TotalPages = totalPages });
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductById(string Id)
        {
            try
            {
                var product = await _productService.GetProductById(Id);
                var stocks = await _stockService.GetStockByIdAsync(Id);
                var material = _dbContext.Materials.Find(product?.MaterialId);
                var sole = _dbContext.Soles.Find(product?.SoleId);
                var brand = _dbContext.Brands.Find(product?.BrandId);
                var category = _dbContext.Categories.Find(product?.CategoryId);

                var stocksDto = new List<StockDto>();
                var images = new List<ProductImageView>();

                foreach (var stock in stocks)
                {
                    var numberSize = await _sizeService.GetByIdSizeAsync(stock.SizeId);

                    var stockDto =  new StockDto()
                    {
                        Id = stock.SizeId,
                        NumberSize = numberSize.NumberSize,
                        UnitInStock = stock.UnitInStock
                    };

                    stocksDto.Add(stockDto);
                }

                var sortedStocksDto = stocksDto.OrderBy(s => s.NumberSize).ToList();

                if (product == null)
                {
                    return NotFound();
                }

                var productItem = new ProductView()
                {
                    Id = product.Id,
                    Name = product.Name ?? string.Empty,
                    Description = product.Description ?? string.Empty,
                    RetailPrice = product.RetailPrice,
                    TaxRate = product.TaxRate ?? decimal.Zero,
                    DiscountType = product.DiscountType,
                    DiscountRate = product.DiscountRate,
                    Image = Url.Action(nameof(GetProductImage), new { id = product.Id }) ?? string.Empty,
                    SoleId = product.SoleId,
                    SoleName = sole?.Name ?? string.Empty,
                    MaterialId = product.MaterialId,
                    MaterialName = material?.Name ?? string.Empty,
                    BrandId = product.BrandId,
                    BrandName = brand?.Name ?? string.Empty,
                    CategoryId = product.CategoryId,
                    CategoryName = category?.Name ?? string.Empty,
                    Status = product.Status,
                    Stocks = sortedStocksDto,
                    Images = images
                };

                return Ok(productItem);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterProductForAdmin([FromQuery] ProductFilterList productDto)
        {
            try
            {
                var products = await _productService.FilterProductForAdmin(productDto);

                var productViews = new List<ProductView>();

                foreach (var product in products)
                {
                    var totalQuantity = await _stockService.TotalQuantityProduct(product.Id);

                    var productView = new ProductView()
                    {
                        Id = product.Id,
                        Name = product.Name ?? string.Empty,
                        Image = Url.Action(nameof(GetProductImage), new { id = product.Id }) ?? string.Empty,
                        RetailPrice = product.RetailPrice,
                        DiscountRate = product.DiscountRate,
                        TotalQuantity = totalQuantity,
                        Status = product.Status
                    };

                    productViews.Add(productView);
                }

                return Ok(productViews);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateProduct([FromForm] ProductDtoForGet productDto)
        {
            if (ModelState.IsValid)
            {
                var res =  await _productService.AddProduct(productDto);


                return Ok(res);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductDtoForGet productDto)
        {
            if(ModelState.IsValid)
            {
                await _productService.UpdateProduct(productDto);

                return NoContent();
            }

            return BadRequest(ModelState);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region recommendersystem

        [HttpGet("similar/{id}")]
        public async Task<IActionResult> GetSimilarProducts(string id)
        {
            try
            {
                var similarProductsView = new List<ProductViewSimilar>();

                var product = await _dbContext.Products.FindAsync(id);

                if (product == null) return NotFound();

                var similarProducts = await _productService.GetSimilarProductAsync(product);

                foreach(var item in similarProducts)
                {
                    var totalQuantity = _dbContext.Stocks.Where(p => p.ProductId == item.Id).Sum(p => p.UnitInStock);

                    var similarProduct = new ProductViewSimilar()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        TotalQuantity = totalQuantity,
                        Image = Url.Action(nameof(GetProductImage), new { id = item.Id }) ?? string.Empty,
                        DiscountRate = item.DiscountRate
                    };

                    similarProductsView.Add(similarProduct);
                }

                return Ok(similarProductsView);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("recommended/{id}")]
        public async Task<ActionResult<IEnumerable<ProductView>>>  GetRecommendedProducts(string id)
        {
            try
            {
                var result = await _productService.GetRecommendProduct(id);

                if (result == null)
                {
                    return NotFound();
                }
                var productViews = new List<ProductView>();

                foreach (var product in result)
                {
                    var totalQuantity = await _stockService.TotalQuantityProduct(product.Id);

                    var productView = new ProductView()
                    {
                        Id = product.Id,
                        Name = product.Name ?? string.Empty,
                        Image = Url.Action(nameof(GetProductImage), new { id = product.Id }) ?? string.Empty,
                        RetailPrice = product.RetailPrice,
                        DiscountRate = product.DiscountRate,
                        TaxRate = product.TaxRate ?? decimal.Zero,
                        DiscountType = product.DiscountType,
                        TotalQuantity = totalQuantity,
                        Status = product.Status
                    };

                    productViews.Add(productView);
                }

                return Ok(productViews);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

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
