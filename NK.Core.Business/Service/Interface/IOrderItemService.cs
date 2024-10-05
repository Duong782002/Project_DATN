namespace NK.Core.Business.Service.Interface
{
    public interface IOrderItemService
    {
        // doanh thu tổng
        Task<decimal> GetTotalAmount();
        Task<decimal> GetTotalBill();

        //doanh thu của tháng hiện tại
        Task<decimal> GetTotalAmountForCurrentMonth();
        Task<decimal> GetTotalBillForCurrentMonth();

        //doanh thu của hôm nay
        Task<decimal> GetTotalAmountForToday();
        Task<decimal> GetTotalBillForToday();

        //doanh thu trong khoảng thời gian
        Task<decimal> GetTotalAmountForTime(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalBillForTime(DateTime startDate, DateTime endDate);

    }
}
