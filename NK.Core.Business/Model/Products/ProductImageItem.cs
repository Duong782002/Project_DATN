using Microsoft.AspNetCore.Http;
namespace NK.Core.Business.Model.Products
{
    public class ProductImageItem
    {
        public string ProductId { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}
