namespace NK.Core.Model.Entities
{
    public class Brand
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public List<Product>? Products { get; set; }
    }
}
