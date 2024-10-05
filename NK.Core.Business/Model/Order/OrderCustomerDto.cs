using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Order
{
    public class OrderCustomerDto
    {
        public string OrderId { get; set; } = string.Empty;
        public StatusOrder Status { get; set; }
    }
}
