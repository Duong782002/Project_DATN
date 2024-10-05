using Microsoft.EntityFrameworkCore;
using NK.Core.Business.Model.Product;
using NK.Core.Business.Model.Products;
using NK.Core.Business.Utilities;
using NK.Core.DataAccess.Repository;
using NK.Core.Model;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;
using System.Net.WebSockets;

namespace NK.Core.Business.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly AppDbContext _dbContext;
        private readonly IGlobalServices _globalServices;
        public ProductService(IProductRepository productRepository, IStockRepository stockRepository, AppDbContext dbContext, IGlobalServices globalServices)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _dbContext = dbContext;
            _globalServices = globalServices;
        }

        public async Task<string> AddProduct(ProductDtoForGet productDto)
        {
            Product product = new Product();
            if(productDto != null)
            {
                product = new Product()
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    RetailPrice = productDto.RetailPrice,
                    DiscountType = productDto.DiscountType,
                    TaxRate = productDto.DiscountType == DiscountType.NONE ? 0 : productDto.TaxRate,
                    FirstQuantity = productDto.Stocks.Sum(s => s.UnitInStock),
                    DiscountRate = productDto.DiscountType == DiscountType.NONE ? productDto.RetailPrice : productDto.RetailPrice - productDto.RetailPrice * productDto.TaxRate / 100,
                    SoleId = productDto.SoleId,
                    BrandId = productDto.BrandId,
                    MaterialId = productDto.MaterialId,
                    CategoryId = productDto.CategoryId,
                    ProductImage = await ImageUtil.GetImageData(productDto.Image)
                };

                await _productRepository.AddProduct(product);

                foreach(var stock in productDto.Stocks)
                {
                    var stockItem = new Stock()
                    {
                        ProductId = product.Id,
                        SizeId = stock.Id,
                        UnitInStock = stock.UnitInStock
                    };

                    await _stockRepository.AddAsync(stockItem);
                }
            }

            return product.Id;
        }

        public async Task UpdateProduct(ProductDtoForGet productDto)
        {
            if(productDto != null)
            {
                var product = await GetProductById(productDto.Id);

                if (product != null)
                {
                    product.Name = productDto.Name;
                    product.Description = productDto.Description;
                    product.RetailPrice = productDto.RetailPrice;
                    product.DiscountType = productDto.DiscountType;
                    product.TaxRate = productDto.DiscountType == DiscountType.NONE ? 0 : productDto.TaxRate;
                    product.DiscountRate = productDto.DiscountType == DiscountType.NONE ? productDto.RetailPrice : productDto.RetailPrice - productDto.RetailPrice * productDto.TaxRate / 100;
                    product.SoleId = productDto.SoleId;
                    product.BrandId = productDto.BrandId;
                    product.MaterialId = productDto.MaterialId;
                    product.CategoryId = productDto.CategoryId;
                    product.ProductImage = productDto.Image != null ? await ImageUtil.GetImageData(productDto.Image) : product.ProductImage;
                    product.Status = productDto.Status;

                    await _productRepository.UpdateProduct(product);

                    if(productDto.Stocks != null && productDto.Stocks.Count > 0)
                    {
                        foreach (var stock in productDto.Stocks)
                        {
                            var stockItem = await _stockRepository.GetStockByProductAndSize(productDto.Id, stock.Id);

                            if (stockItem != null)
                            {
                                stockItem.UnitInStock = stock.UnitInStock;
                                await _stockRepository.UpdateAsync(stockItem);
                            }
                            else
                            {
                                var stockAdd = new Stock()
                                {
                                    ProductId = productDto.Id,
                                    SizeId = stock.Id,
                                    UnitInStock = stock.UnitInStock
                                };

                                await _stockRepository.AddAsync(stockAdd);
                            }
                        }
                    }

                }

            }
        }

        public async Task<bool> DeleteProduct(string id)
        {
            // Tìm sản phẩm theo id
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null) return false;

            // Lấy danh sách các stock liên quan tới sản phẩm
            var stocks = await _dbContext.Stocks.Where(p => p.ProductId == id).ToListAsync();

            // Xóa các stock liên quan tới sản phẩm
            if (stocks.Count > 0)
            {
                _dbContext.Stocks.RemoveRange(stocks);
            }

            // Xóa sản phẩm
            _dbContext.Products.Remove(product);

            // Lưu các thay đổi
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {

            return await _dbContext.Products.Include(p => p.Stocks).ToListAsync();
        }

        public async Task<Product?> GetProductById(string id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>?> FilterProductForAdmin(ProductFilterList productDto)
        {
            var query = await _dbContext.Products.Where(p => 
                                                           (string.IsNullOrEmpty(productDto.SoleId)|| p.SoleId == productDto.SoleId)
                                                        && (string.IsNullOrEmpty(productDto.MaterialId) || p.MaterialId == productDto.MaterialId)
                                                        && (string.IsNullOrEmpty(productDto.BrandId) || p.BrandId == productDto.BrandId)
                                                        && (string.IsNullOrEmpty(productDto.CategoryId) || p.CategoryId == productDto.CategoryId)
                                                        && (p.Status == productDto.Status)
                                                        && (productDto.FromMoney == null || productDto.FromMoney <= p.DiscountRate)
                                                        && (productDto.ToMoney == null || p.DiscountRate <= productDto.ToMoney)).ToListAsync();

            return query;
        }

        public async Task<IList<Product>?> FilterProductForCustomer(ProductFilterOptionAPI filter)
        {
            var query = await GetAllProduct();

            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                query = query.Where(p => p.Name.ToLower().Contains(filter.Keyword.ToLower()) ||
                                         p.Description.ToLower().Contains(filter.Keyword.ToLower()));
            }

            if(filter.Categories != null && filter.Categories.Any())
            {
                query = query.Where(p => filter.Categories.Contains(p.CategoryId));
            }

            if(filter.Brands != null && filter.Brands.Any())
            {
                query = query.Where(p => filter.Brands.Contains(p.BrandId));
            }

            if (filter.Sizes != null && filter.Sizes.Any())
            {
                query = query.Where(p => p.Stocks != null && p.Stocks.Any(s => filter.Sizes.Contains(s.SizeId)));
            }

            if (filter.Materials != null && filter.Materials.Any())
            {
                query = query.Where(p => filter.Materials.Contains(p.MaterialId));
            }

            if(filter.Soles != null && filter.Soles.Any())
            {
                query = query.Where(p => filter.Soles.Contains(p.SoleId));
            }

            if(!string.IsNullOrEmpty(filter.Min) && !string.IsNullOrEmpty(filter.Max))
            {
                query = query.Where(p => p.DiscountRate >= decimal.Parse(filter.Min) && p.DiscountRate <= decimal.Parse(filter.Max));
            }

            if (filter.SortBy.HasValue)
            {
                switch (filter.SortBy.Value)
                {
                    case SortBy.NEWEST:
                        query = query.OrderByDescending(p => p.CreatedDate);
                        break;
                    case SortBy.ASCENDING:
                        query = query.OrderBy(p => p.DiscountRate).ThenBy(p => p.RetailPrice);
                        break;
                    case SortBy.DESCENDING:
                        query = query.OrderByDescending(p => p.DiscountRate).ThenByDescending(p => p.RetailPrice);
                        break;
                    default: 
                        break;
                }
            }

            return query.ToList();
        }

        public async Task<IEnumerable<Product>> GetProductCount(int quantity)
        {
            var query = await _dbContext.Products
                                 .OrderByDescending(p => p.TaxRate)
                                 .Take(quantity)
                                 .ToListAsync();

            return query;
        }

        #region Content Filtering similar
        public async Task<IEnumerable<Product>> GetSimilarProductAsync(Product product, int numberOfProducts = 5)
        {
            var products = await _dbContext.Products
                            .Where(p => p.Id != product.Id)
                            .ToListAsync();

            var similarProducts = products
                            .Select(p => new
                            {
                                Product = p,
                                SimilarityScore = CalculateCosineSimilarity(product, p)
                            })
                            .OrderByDescending(x => x.SimilarityScore)
                            .Take(numberOfProducts)
                            .Select(x => x.Product)
                            .ToList();

            return similarProducts;
        }

        private static double CalculateCosineSimilarity(Product product1, Product product2)
        {
            int[] vector1 = CreateFeatureVector(product1);
            int[] vector2 = CreateFeatureVector(product2);

            double dotProduct = vector1.Zip(vector2, (x, y) => x * y).Sum();
            double magnitude1 = Math.Sqrt(vector1.Sum(x => x * x));
            double magnitude2 = Math.Sqrt(vector2.Sum(x => x * x));

            if (magnitude1 == 0 || magnitude2 == 0)
                return 0;

            return dotProduct / (magnitude1 * magnitude2);
        }

        private static int[] CreateFeatureVector(Product product)
        {
            return new int[]
            {
                product.SoleId == null ? 0 : 1,
                product.MaterialId == null ? 0 : 1,
                product.CategoryId == null ? 0 : 1,
                product.BrandId == null ? 0 : 1,
                (int)product.weather
            };
        }
        #endregion

        #region Collaborative Filtering

        public async Task<IEnumerable<Product>> GetRecommendProduct(string userId)
        {
            var result = new List<Product>();

            var userWishLists = await _dbContext.WishLists
                                .Where(w => w.UserId == userId)
                                .GroupBy(w => w.ProductsId)
                                .Select(w => new { ProductId = w.Key, Count = w.Count() })
                                .ToListAsync();

            var userOrderItems = await _dbContext.OrderItems
                                    .Where(o => o.Order.UserId == userId)
                                    .GroupBy(o => o.ProductId)
                                    .Select(o => new { ProductId = o.Key, Count = o.Count() })
                                    .ToListAsync();

            if(_globalServices.IsFirst || !userWishLists.Any() && !userOrderItems.Any())
            {
                result = await _dbContext.Products
                                    .OrderByDescending(p => p.CreatedDate)
                                    .Take(12)
                                    .ToListAsync();

                return result;
            }
            else
            {
                var wishListsRecommend = RecommendWishlists(userId);
                result.AddRange(wishListsRecommend);

                if(result.Count < 12)
                {
                    var orderItemsRecommend = RecommendOrderItems(userId);

                    foreach(var item in orderItemsRecommend)
                    {
                        if(!result.Any(p => p.Id == item.Id))
                        {
                            result.Add(item);
                        }
                    }
                }

                if(result.Count < 12)
                {
                    var latestProducts = await _dbContext.Products
                                                .OrderByDescending(p => p.CreatedDate)
                                                .ToListAsync();

                    foreach(var item in latestProducts)
                    {
                        if(!result.Any(p => p.Id == item.Id))
                        {
                            result.Add(item);
                        }

                        if (result.Count >= 12) break;
                    }
                }

                return result.Take(12).ToList();
            }

        }

        public List<Product> RecommendWishlists(string userId)
        {
            var userWishLists = _dbContext.WishLists
                                .Where(w => w.UserId == userId)
                                .GroupBy(w => w.ProductsId)
                                .Select(w => new { ProductId = w.Key, Count = w.Count() })
                                .ToList();

            var allUsers = _dbContext.WishLists
                               .Where(w => w.UserId != userId)
                               .GroupBy(w => w.UserId)
                               .ToList();

            var similarities = new Dictionary<string, double>();
            foreach(var otherUser in allUsers)
            {
                var otherUserId = otherUser.Key;
                var otherUserWishLists = otherUser
                             .GroupBy(w => w.ProductsId)
                             .Select(w => new { ProductId = w.Key, Count = w.Count() })
                             .ToList();
                var similarity = CalculateCosineSimilarityProducts(userWishLists.Select(w => w.ProductId).ToList(), otherUserWishLists.Select(w => w.ProductId).ToList()); ;
                similarities.Add(otherUserId, similarity);
            }

            var sortedUsers = similarities.OrderByDescending(pair => pair.Value).ToList();
            var recommendedProducts = new List<Product>();

            foreach(var pair in sortedUsers)
            {
                var otherUserId = pair.Key;
                var similarity = pair.Value;

                if(similarity > 0)
                {
                    var unPurchaseProduct = GetUnWishlists(userId, otherUserId);

                    foreach(var product in unPurchaseProduct)
                    {
                        if (!recommendedProducts.Contains(product))
                        {
                            recommendedProducts.Add(product);
                        }
                    }
                }
            }

            return recommendedProducts;
        }

        public List<Product> RecommendOrderItems(string userId)
        {
            var userOrderItems = _dbContext.OrderItems
                                    .Where(o => o.Order.UserId == userId)
                                    .GroupBy(o => o.ProductId)
                                    .Select(o => new { ProductId = o.Key, Count = o.Count() })
                                    .ToList();

            var allUsers = _dbContext.OrderItems
                                    .Where(o => o.Order.UserId != userId)
                                    .GroupBy(o => o.Order.UserId)
                                    .ToList();

            var similarities = new Dictionary<string, double>();
            foreach(var otherUser in allUsers)
            {
                var otherUserId = otherUser.Key;
                var otherUserOrderItems = otherUser
                                        .GroupBy(o => o.ProductId)
                                        .Select(o => new { ProductId = o.Key, Count = o.Count() })
                                        .ToList();
                var similarity = CalculateCosineSimilarityProducts(userOrderItems.Select(o => o.ProductId).ToList(), otherUserOrderItems.Select(o => o.ProductId).ToList());
                similarities.Add(otherUserId, similarity);
            }

            var sortedUsers = similarities.OrderByDescending(pair => pair.Value).ToList();
            var recommendedProducts = new List<Product>();

            foreach(var pair in sortedUsers)
            {
                var otherUserId = pair.Key;
                var similarity = pair.Value;

                if(similarity > 0)
                {
                    var unPurchaseProduct = GetUnOrderItems(userId, otherUserId);

                    foreach(var product in unPurchaseProduct)
                    {
                        if (!recommendedProducts.Contains(product))
                        {
                            recommendedProducts.Add(product);
                        }
                    }
                }
            }
            return recommendedProducts;
        }

        private List<Product> GetUnWishlists(string targetUserId,string otherUserId)
        {
            //lay danh sach san pham ma nguoi dung muc tieu da thich
            var targetUserLikedProducts = _dbContext.WishLists
                                            .Where(w => w.UserId == targetUserId)
                                            .Select(w => w.ProductsId)
                                            .ToList();

            //lay danh sach san pham ma nguoi dung khac da thich nhung nguoi dung muc tieu chua thich
            var otherUserLikedProducts = _dbContext.WishLists
                                            .Where(w => w.UserId == otherUserId)
                                            .Select(w => w.ProductsId)
                                            .ToList();

            var unPurchasedProductIds = otherUserLikedProducts
                                            .Where(productId => !targetUserLikedProducts.Contains(productId))
                                            .Distinct() .ToList();

            var unPurchaseProducts = _dbContext.Products
                                            .Where(p => unPurchasedProductIds.Contains(p.Id))
                                            .ToList();

            return unPurchaseProducts;
        }

        private List<Product> GetUnOrderItems(string targetUserId,string otherUserId)
        {
            //lay danh sach san pham ma nguoi dung muc tieu da thich
            var targetuserLikedProducts = _dbContext.OrderItems
                                            .Where(o => o.Order.UserId == targetUserId)
                                            .Select(o => o.ProductId)
                                            .ToList();

            //lay danh sach san pham ma nguoi dung khac da thich nhung nguoi dung muc tieu chua thich
            var otherUserLikedProducts = _dbContext.OrderItems
                                            .Where(o => o.Order.UserId == otherUserId)
                                            .Select(o => o.ProductId)
                                            .ToList();

            var unPurchasedProductIds = otherUserLikedProducts
                                            .Where(productId => !targetuserLikedProducts.Contains(productId))
                                            .Distinct().ToList();

            var unPurchaseProducts = _dbContext.Products
                                            .Where(p => unPurchasedProductIds.Contains(p.Id))
                                            .ToList();

            return unPurchaseProducts;
        }

        private static double CalculateCosineSimilarityProducts(List<string> list1, List<string> list2)
        {
            var allProducts = list1.Union(list2).ToList();

            double dotProduct = 0;
            double norm1 = 0;
            double norm2 = 0;

            foreach(var product in allProducts)
            {
                int a = list1.Contains(product) ? 1 : 0;
                int b = list2.Contains(product) ? 1 : 0;

                dotProduct += a * b;
                norm1 += a * a;
                norm2 += b * b;
            }

            if(norm1 == 0 || norm2 == 0)
            {
                return 0;
            }
            else
            {
                return dotProduct / (norm1 * norm2);
            }
        }
        #endregion
    }
}
