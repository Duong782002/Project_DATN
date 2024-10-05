namespace NK.Core.Business.Model.Size
{
    public class SizeDto
    {
        public int NumberSize { get; set; }
    }

    public class SizeDtoUpdate : SizeDto
    {
        public string Id { get; set; } = string.Empty;
    }
}
