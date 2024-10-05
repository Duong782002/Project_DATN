using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Order
{
    public class OrderStatusDto
    {
        public string OrderId { get; set; } = string.Empty;
        public StatusOrder Status { get; set; }
        public DateTime Time { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
