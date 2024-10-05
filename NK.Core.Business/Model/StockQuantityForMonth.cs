namespace NK.Core.Business.Model
{
    public class StockQuantityForMonth
    {
        public int Month { get; set; }
        public int Warehouse {  get; set; }
        public int Inventory { get; set; }
        public int Import { get; set; }
    }
}
