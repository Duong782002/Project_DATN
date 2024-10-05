using NK.Core.Model.Entities;

namespace NK.Core.DataAccess.Repository
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task<IEnumerable<Product>> GetAllProduct();
    }
}
