using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NK.Core.Business.Model.Order;
using NK.Core.Business.Model.Product;
using NK.Core.Business.Service;
using NK.Core.Model;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;
using Stripe.Checkout;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly AppDbContext _dbContext;
        public OrderController(IOrderService orderService, AppDbContext dbContext, IProductService productService)
        {
            _orderService = orderService;
            _dbContext = dbContext;
            _productService = productService;
        }

        #region ADMIN
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            try
            {
                var order = await _orderService.GetAllOrderAsync();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetConfirmOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetConfirmOrders()
        {
            try
            {
                var order = await _orderService.GetAllOrderAsync();

                var finalResults = order.Where(p => p.CurrentStatus == StatusOrder.CONFIRM);

                return Ok(finalResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetPendingShipperOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetPendingShipperOrders()
        {
            try
            {
                var order = await _orderService.GetAllOrderAsync();

                var finalResults = order.Where(p => p.CurrentStatus == StatusOrder.PENDING_SHIP);

                return Ok(finalResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetShippingOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetShippingOrders()
        {
            try
            {
                var order = await _orderService.GetAllOrderAsync();

                var finalResults = order.Where(p => p.CurrentStatus == StatusOrder.SHIPPING);

                return Ok(finalResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetDeliveredOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetDeliveredOrders()
        {
            try
            {
                var order = await _orderService.GetAllOrderAsync();

                var finalResults = order.Where(p => p.CurrentStatus == StatusOrder.DELIVERIED);

                return Ok(finalResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetCancelOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetCancelOrders()
        {
            try
            {
                var order = await _orderService.GetAllOrderAsync();

                var finalResults = order.Where(p => p.CurrentStatus == StatusOrder.CANCELED);

                return Ok(finalResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrdersDetailById(string id)
        {
            try
            {
                var orderDetail = await _dbContext.Orders
                            .Where(order => order.Id == id)
                            .Select(order => new OrderDto()
                            {
                                Id = order.Id,
                                CustomerName = order.CustomerName,
                                AddressName = order.AddressName,
                                PhoneNumber = order.PhoneNumber,
                                Note = order.Note,
                                TotalAmount = order.TotalAmount,
                                CurrentStatus = order.CurrentStatus,
                                Payment = order.Payment,
                                DateCreated = order.DateCreated,
                                PassivedDate = order.PassivedDate,
                                ModifiedDate = order.ModifiedDate,
                                UserId = order.UserId,
                                FullName = order.AppUser != null ? order.AppUser.UserName : string.Empty,
                                OrderItems = order.OrderItems.Select(item => new OrderItemDto()
                                {
                                    OrderId = item.Id,
                                    ProductId = item.ProductId,
                                    ProductName = item.Product != null ? item.Product.Name ?? string.Empty : string.Empty,
                                    DiscountRate = item.Product != null ? item.Product.DiscountRate : decimal.Zero,
                                    ProductImage = Url.Action(nameof(GetProductImage), new { id = item.ProductId }) ?? string.Empty,
                                    SizeId = item.SizeId,
                                    NumberSize = item.Size != null ? item.Size.NumberSize : int.MinValue,
                                    Quantity = item.Quantity,
                                    Price = item.Quantity * item.Product.DiscountRate
                                }).ToList(),
                                OrderStatuses = order.OrderStatuses.Select(status => new OrderStatusDto()
                                {
                                    OrderId = status.Id,
                                    Status = status.Status,
                                    Time = status.Time,
                                    Note = status.Note ?? string.Empty
                                }).ToList()
                            })
                            .FirstOrDefaultAsync();
                var res = _dbContext.AppUsers.Where(p => p.Id == orderDetail.UserId).First();
                orderDetail.FullName = res.UserName;

                return Ok(orderDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost("OrderStatus")]
        public async Task<IActionResult> UpdateStatusOrder(OrderStatusDto orderStatusDto)
        {
            using(var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var orderStatus = new OrderStatus()
                    {
                        OrderId = orderStatusDto.OrderId,
                        Status = orderStatusDto.Status,
                        Time = orderStatusDto.Time,
                        Note = orderStatusDto.Note
                    };

                    var order = _dbContext.Orders.Find(orderStatusDto.OrderId);
                    if(order != null)
                    {
                        order.CurrentStatus = orderStatusDto.Status;
                        _dbContext.Orders.Update(order);
                    }

                    await _dbContext.OrderStatuses.AddAsync(orderStatus);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return NoContent();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        [HttpPost("createnewatstore")]
        public async Task<IActionResult> CreateNewAtStore([FromBody] OrderPaymentAtStore order)
        {
            try
            {
                await _orderService.CreateNewOrderAtStore(order);

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Stripe

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOutStripe([FromBody] OrderDetailDto listOrderDetail)
        {
            try
            {
                var domain = "http://localhost:4200/";

                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + "customer/success",
                    CancelUrl = domain + "customer/purchase",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };

                foreach(var item in listOrderDetail.Orders)
                {
                    var productId = ConvertToProductId(item.Image);

                    var sessionListItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)item.DiscountRate,
                            Currency = "vnd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.ProductName.ToString()
                            }
                        },
                        Quantity = (long)item.Quantity
                    };
                    options.LineItems.Add(sessionListItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);

                return Ok(new { session = session.Url });

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region customer
        [HttpPost("createneworder")]
        public async Task<IActionResult> CreateNewOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            try
            {
                await _orderService.CreateNewOnlineOrder(orderCreateDto);

                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("orderdetailcustomer")]
        public async Task<IActionResult> GetOrderDetailByUser(string userId, StatusOrder status)
        {
            try
            {
                var listOrderDetail = await _dbContext.Orders
                            .Where(order => order.UserId == userId && order.CurrentStatus == status)
                            .Select(order => new OrderDto()
                            {
                                Id = order.Id,
                                CustomerName = order.CustomerName,
                                AddressName = order.AddressName,
                                PhoneNumber = order.PhoneNumber,
                                Note = order.Note,
                                TotalAmount = order.TotalAmount,
                                Payment = order.Payment,
                                CurrentStatus = order.CurrentStatus,
                                DateCreated = order.DateCreated,
                                PassivedDate = order.PassivedDate,
                                ModifiedDate = order.ModifiedDate,
                                FullName = string.Empty,
                                OrderItems = order.OrderItems.Select(item => new OrderItemDto()
                                {
                                    OrderId = item.Id,
                                    ProductId = item.ProductId,
                                    ProductName = item.Product != null ? item.Product.Name ?? string.Empty : string.Empty,
                                    DiscountRate = item.Product != null ? item.Product.DiscountRate : decimal.Zero,
                                    ProductImage = Url.Action(nameof(GetProductImage), new { id = item.ProductId }) ?? string.Empty,
                                    SizeId = item.SizeId,
                                    NumberSize = item.Size != null ? item.Size.NumberSize : int.MinValue,
                                    Quantity = item.Quantity,
                                    Price = item.Quantity * item.Product.DiscountRate
                                }).ToList(),
                                OrderStatuses = order.OrderStatuses.Select(status => new OrderStatusDto()
                                {
                                    OrderId = status.Id,
                                    Status = status.Status,
                                    Time = status.Time,
                                    Note = status.Note ?? string.Empty
                                }).ToList()
                            })
                            .ToListAsync();

                // Lặp qua từng đơn hàng trong danh sách và kiểm tra gán giá trị Note
                foreach (var orderDetail in listOrderDetail)
                {
                    foreach (var orderStatus in orderDetail.OrderStatuses)
                    {
                        if (orderStatus.Status == orderDetail.CurrentStatus)
                        {
                            orderDetail.Note = orderStatus.Note ?? string.Empty;
                            break; // Nếu đã tìm thấy trạng thái khớp, không cần duyệt tiếp
                        }
                    }
                }

                return Ok(listOrderDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("cancelorder")]
        public async Task<IActionResult> CancelOrder([FromBody] OrderCustomerDto orderStatusDto)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var orderStatus = new OrderStatus()
                {
                    OrderId = orderStatusDto.OrderId,
                    Status = orderStatusDto.Status,
                    Time = DateTime.Now,
                    Note = "Đơn hàng đã bị hủy"
                };

                var order = _dbContext.Orders.Find(orderStatusDto.OrderId);
                if (order != null)
                {
                    order.CurrentStatus = orderStatusDto.Status;
                    _dbContext.Orders.Update(order);
                    await _dbContext.SaveChangesAsync();

                    var listOrderItem = await _dbContext.OrderItems.Where(p => p.OrderId == order.Id).ToListAsync();
                    if (listOrderItem != null)
                    {
                        foreach (var item in listOrderItem)
                        {
                            var stock = await _dbContext.Stocks.Where(p => p.ProductId == item.ProductId
                                                                         && p.SizeId == item.SizeId).FirstOrDefaultAsync();

                            if (stock != null)
                            {
                                stock.UnitInStock += (int)item.Quantity;

                                _dbContext.Stocks.Update(stock);
                                await _dbContext.SaveChangesAsync();
                            }
                        }
                    }
                }
                await _dbContext.OrderStatuses.AddAsync(orderStatus);
                await _dbContext.SaveChangesAsync();

                transaction.Commit();
                return NoContent();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        #endregion

        // GET: api/products/1/image
        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetProductImage(string id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null || product.ProductImage == null)
            {
                return NotFound();
            }

            return File(product.ProductImage, "image/jpeg");
        }

        [HttpGet("converttoproductid")]
        public string ConvertToProductId(string image)
        {
            string[] parts = image.Split('/');
            string thirdPart = parts[3];

            return thirdPart;
        }
    }
}
