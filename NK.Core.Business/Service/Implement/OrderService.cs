using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NK.Core.Business.Model.Order;
using NK.Core.Model;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;

namespace NK.Core.Business.Service
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;
        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<OrderDto>> GetAllOrderAsync()
        {
            var orderDtoList = await _dbContext.Orders
                                                .Select(item => new OrderDto()
                                                {
                                                    Id = item.Id,
                                                    CustomerName = item.CustomerName ?? string.Empty,
                                                    PhoneNumber = item.PhoneNumber ?? string.Empty,
                                                    DateCreated = item.DateCreated,
                                                    TotalAmount = item.TotalAmount,
                                                    CurrentStatus = item.CurrentStatus
                                                }).ToListAsync();

            return orderDtoList;

        }
        public async Task CreateNewOnlineOrder(OrderCreateDto orderCreateDto)
        {
            var checkAddress = await _dbContext.Addresses.Where(p => p.CityCode == orderCreateDto.District
                                                                 &&  p.WardCode == orderCreateDto.Ward
                                                                 &&  p.ProvinceCode == orderCreateDto.Province)
                                                         .FirstOrDefaultAsync();
            var carts = await _dbContext.ShoppingCartItems.ToListAsync();

            #region Them don hang online
            if (checkAddress == null)
            {
                var address = new Address()
                {
                    FullName = orderCreateDto.CustomerName ?? string.Empty,
                    AddressLine = orderCreateDto.AddressLine,
                    CityCode = orderCreateDto.District,
                    ProvinceCode = orderCreateDto.Province,
                    WardCode = orderCreateDto.Ward,
                    PhoneNumber = orderCreateDto.PhoneNumber,
                    SetAsDefault = false,
                    UserId = orderCreateDto.UserId
                };

                await _dbContext.Addresses.AddAsync(address);
                await _dbContext.SaveChangesAsync();

                checkAddress = address;
            }

            var order = new Order()
            {
                CustomerName = orderCreateDto.CustomerName,
                AddressName = orderCreateDto.AddressLine + ", " + orderCreateDto.WardName + ", " + orderCreateDto.DistrictName + ", " + orderCreateDto.Provincename,
                PhoneNumber = orderCreateDto.PhoneNumber,
                Note = string.Empty,
                TotalAmount = orderCreateDto.TotalAmount,
                Payment = orderCreateDto.Payment,
                CurrentStatus = StatusOrder.CONFIRM,
                UserId = orderCreateDto.UserId,
                AddressId = checkAddress.Id,
                OrderStatuses = new List<OrderStatus>()
                {
                    new OrderStatus()
                    {
                        Status = StatusOrder.CONFIRM,
                        Time = DateTime.Now,
                        Note = "Chờ xác nhận"
                    }
                },
                OrderItems = orderCreateDto.OrderItems.Select(item => new OrderItem()
                {
                    ProductId = ConvertToProductId(item.Image) ?? string.Empty,
                    SizeId = ConvertToSizeId(item.NumberSize) ?? string.Empty,
                    Quantity = item.Quantity,
                    UnitPrice = ConvertToPrice(item.Image) * item.Quantity
                }).ToList()
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            #endregion

            #region Update Stock
            if(order != null)
            {
                var stockList = await GetOrderItemsQuantityAsync(order.OrderItems);
                _dbContext.Stocks.UpdateRange(stockList);
                await _dbContext.SaveChangesAsync();
            }
            #endregion
        }
        public async Task<List<Stock>> GetOrderItemsQuantityAsync(IEnumerable<OrderItem> orderItems)
        {
            var stockList = new List<Stock>();

            foreach(var item in orderItems)
            {
                var currentUnitInStock = await _dbContext.Stocks.Where(p => p.ProductId == item.ProductId
                                                                         && p.SizeId == item.SizeId)
                                                                .FirstOrDefaultAsync();

                if (currentUnitInStock == null) throw new System.Exception($"There are something wrong! Could not find the stock with ProducId {item.ProductId}, SizeId {item.SizeId}.");
                currentUnitInStock.UnitInStock -= (int)item.Quantity;
                stockList.Add(currentUnitInStock);
            }

            return stockList;
        }

        public string ConvertToProductId(string image)
        {
            string[] parts = image.Split('/');
            string thirdPart = parts[3];

            return thirdPart;   
        }

        public string ConvertToSizeId(int numberSize)
        {
            var sizes = _dbContext.Sizes.ToList();

            return sizes.Where(p => p.NumberSize == numberSize).FirstOrDefault()?.Id ?? string.Empty;
        }

        public decimal ConvertToPrice(string image)
        {
            string[] parts = image.Split('/');
            string thirdPart = parts[3];

            var productes = _dbContext.Products.Where(p => p.Id == thirdPart).FirstOrDefault();

            return productes?.DiscountRate ?? decimal.Zero;
        }

        public string ConvertToFullName(string userId)
        {
            var res = _dbContext.AppUsers.Where(p => p.Id == userId).First();

            return res.UserName;
        }

        public async Task CreateNewOrderAtStore(OrderPaymentAtStore orderPaymentAtStore)
        {
            try
            {
                #region order
                var order = new Order()
                {
                    CustomerName = orderPaymentAtStore.CustomerName,
                    PhoneNumber = orderPaymentAtStore.PhoneNumber,
                    AddressName = orderPaymentAtStore.AddressLine,
                    Note = orderPaymentAtStore.Note,
                    Payment = PaymentMethod.OTHER,
                    CurrentStatus = StatusOrder.DELIVERIED,
                    TotalAmount = orderPaymentAtStore.TotalAmount,
                    UserId = orderPaymentAtStore.UserId,
                    OrderStatuses = new List<OrderStatus>()
                    {
                        new OrderStatus()
                        {
                            Status = StatusOrder.CONFIRM,
                            Time = DateTime.Now,
                            Note = "Chờ xác nhận"
                        },
                        new OrderStatus()
                        {
                            Status = StatusOrder.DELIVERIED,
                            Time = DateTime.Now,
                            Note = "Thành công"
                        }
                    },
                    OrderItems = orderPaymentAtStore.OrderItems.Select(p => new OrderItem()
                    {
                        ProductId = p.ProductId,
                        SizeId = p.SizeId,
                        Quantity = p.Quantity,
                        UnitPrice = p.UnitPrice
                    }).ToList()
                };

                await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();
                #endregion

                #region Update Stock
                if (order != null)
                {
                    var stockList = await GetOrderItemsQuantityAsync(order.OrderItems);
                    _dbContext.Stocks.UpdateRange(stockList);
                    await _dbContext.SaveChangesAsync();
                }
                #endregion
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
