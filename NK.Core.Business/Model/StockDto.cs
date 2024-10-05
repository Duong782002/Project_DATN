using NK.Core.Business.Model.Product;

namespace NK.Core.Business.Model
{
    public class StockDto
    {
        public string Id { get; set; } = string.Empty;
        public int NumberSize { get; set; }
        public int UnitInStock { get; set; }
    }
}
