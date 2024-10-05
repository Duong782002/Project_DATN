using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Product
{
    public class ProductFilterOptionAPI
    {
        public string Keyword { get; set; } = string.Empty;
        public List<string>? Categories { get; set; }
        public List<string>? Brands { get; set; }
        public List<string>? Sizes { get; set; }
        public string Min { get; set; } = string.Empty;
        public string Max { get; set; } = string.Empty;
        public List<string>? Materials { get; set; }
        public List<string>? Soles { get; set; }
        public SortBy? SortBy { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 9;
    }
}
