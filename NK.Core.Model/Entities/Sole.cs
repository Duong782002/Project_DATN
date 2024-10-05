namespace NK.Core.Model.Entities
{
    public class Sole
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public List<Product>? Products { get; set; }
    }
}
