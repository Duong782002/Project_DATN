using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NK.Core.Business.Model.Product;
using NK.Core.Business.Service;
using NK.Core.Business.Service.Interface;
using NK.Core.Model;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;
using System;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IProductService _productService;
        private readonly AppDbContext _dbContext;

        public OrderItemController(IOrderItemService orderItemService, AppDbContext dbContext, IProductService productService)
        {
            _orderItemService = orderItemService;
            _dbContext = dbContext;
            _productService = productService;
        }

        //doanh thu tong
        [HttpGet("totalAmountAll")]
        public async Task<IActionResult> GetTotalAmount()
        {
            try
            {
                decimal totalAmount = await _orderItemService.GetTotalAmount();
                return Ok(totalAmount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("totalBillAll")]
        public async Task<IActionResult> GetTotalBill()
        {
            try
            {
                decimal totalBill = await _orderItemService.GetTotalBill();
                return Ok(totalBill);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //doanh thu cua thanh hien tai
        [HttpGet("totalAmountMonth")]
        public async Task<IActionResult> GetTotalAmountForMonth()
        {
            try
            {
                decimal totalAmount = await _orderItemService.GetTotalAmountForCurrentMonth();
                return Ok(totalAmount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("totalBillMonth")]
        public async Task<IActionResult> GetTotalBillForMonth()
        {
            try
            {
                decimal totalBill = await _orderItemService.GetTotalBillForCurrentMonth();
                return Ok(totalBill);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //doanh thu cua ngay hien tai
        [HttpGet("totalAmountToday")]
        public async Task<IActionResult> GetTotalAmountForToday()
        {
            try
            {
                decimal totalAmount = await _orderItemService.GetTotalAmountForToday();
                return Ok(totalAmount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("totalBillToday")]
        public async Task<IActionResult> GetTotalBillForToday()
        {
            try
            {
                decimal totalBill = await _orderItemService.GetTotalBillForToday();
                return Ok(totalBill);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //doanh thu trong khoang thoi gian
        [HttpGet("totalAmountTime")]
        public async Task<IActionResult> GetTotalAmountForTime(DateTime startDate, DateTime endDate)
        {
            try
            {
                decimal totalAmount = await _orderItemService.GetTotalAmountForTime(startDate, endDate);
                return Ok(totalAmount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("totalBillTime")]
        public async Task<IActionResult> GetTotalBillForTime(DateTime startDate, DateTime endDate)
        {
            try
            {
                decimal totalBill = await _orderItemService.GetTotalBillForTime(startDate, endDate);
                return Ok(totalBill);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //top sản phẩm bán chạy
        [HttpGet("getTop")]
        public async Task<ActionResult<List<ProductTopSales>>> GetTop()
        {
            try
            {
                var orderItems = await _dbContext.OrderItems
                    .Include(p => p.Order)
                    .Where(p => p.Order.CurrentStatus == StatusOrder.DELIVERIED)
                    .ToListAsync();

                var topProducts = orderItems
                    .GroupBy(p => p.ProductId)
                    .Select(g => new
                    {
                        ProductId = g.Key,
                        TotalQuantity = g.Sum(p => p.Quantity),
                        TotalPrice = g.Sum(p => p.Quantity * ConvertToPrice(p.ProductId))
                    })
                    .OrderByDescending(p => p.TotalQuantity)
                    .Take(10)
                    .ToList();

                var productsId = topProducts.Select(p => p.ProductId).ToList();

                var productsName = await _dbContext.Products
                    .Where(p => productsId.Contains(p.Id))
                    .ToDictionaryAsync(p => p.Id, p => p.Name);

                var results = topProducts
                    .Select(item => new ProductTopSales()
                    {
                        ProductId = item.ProductId,
                        ProductName = productsName.ContainsKey(item.ProductId) ? productsName[item.ProductId] : string.Empty,
                        ProductImage = Url.Action(nameof(GetProductImage), new { id = item.ProductId }) ?? string.Empty,
                        TotalQuantitySold = item.TotalQuantity,
                        TotalRevenue = item.TotalPrice,
                    })
                    .ToList();

                return Ok(results);
            }
            catch (Exception)
            {
                throw;
            }
        }

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

        [HttpGet("convert")]
        public decimal ConvertToPrice(string id)
        {
            var products = _dbContext.Products.Where(p => p.Id == id).First();

            return products.DiscountRate;
        }
    }
}
