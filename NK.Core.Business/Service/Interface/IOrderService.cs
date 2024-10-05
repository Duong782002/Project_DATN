using NK.Core.Business.Model.Order;

namespace NK.Core.Business.Service
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrderAsync();
        Task CreateNewOnlineOrder(OrderCreateDto orderCreateDto);
        Task CreateNewOrderAtStore(OrderPaymentAtStore orderPaymentAtStore);
        string ConvertToProductId(string image);
        string ConvertToFullName(string userId);
    }
}
