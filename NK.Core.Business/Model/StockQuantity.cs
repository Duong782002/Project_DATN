namespace NK.Core.Business.Model
{
    public class StockQuantity
    {
        public decimal QuantityWarehouse { get; set; }
        public decimal QuantityInventory { get; set; }
        public decimal QuantityImport { get; set; }
        public decimal PercentWarehouse { get; set; }
        public decimal PercentInventory { get; set;}
        public decimal PercentImport { get; set; }
    }
}
