using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Product
{
    public class ProductFilterList
    {
        public string? SoleId { get; set; }
        public string? MaterialId { get; set; } 
        public string? BrandId { get; set; }
        public string? CategoryId { get; set; }
        public decimal? FromMoney { get; set; }
        public decimal? ToMoney { get; set; }
        public Status Status { get; set; } = Status.ACTIVE;
    }
}
