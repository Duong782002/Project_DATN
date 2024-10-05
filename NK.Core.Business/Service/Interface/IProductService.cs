using NK.Core.Business.Model.Product;
using NK.Core.Business.Model.Products;
using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public interface IProductService
    {
        Task<string> AddProduct(ProductDtoForGet productDto);
        Task UpdateProduct(ProductDtoForGet productDto);
        Task<Product?> GetProductById(string id);
        Task<IEnumerable<Product>> GetAllProduct();
        Task<bool> DeleteProduct(string id);
        Task<IEnumerable<Product>?> FilterProductForAdmin(ProductFilterList productDto);
        Task<IList<Product>?> FilterProductForCustomer(ProductFilterOptionAPI filter);
        Task<IEnumerable<Product>> GetProductCount(int quantity);
        Task<IEnumerable<Product>> GetSimilarProductAsync(Product product1, int numberOfProducts = 5);
        Task<IEnumerable<Product>> GetRecommendProduct(string userId);
    }
}
