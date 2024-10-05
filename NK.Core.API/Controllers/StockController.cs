using Microsoft.AspNetCore.Mvc;
using NK.Core.Business.Model;
using NK.Core.Business.Service;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetAllStocks()
        {
            try
            {
                var stocks = await _stockService.GetAllStocksAsync();

                if (stocks == null || !stocks.Any())
                {
                    return NotFound();
                }

                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Stock>> GetStock(string productId)
        {
            var stock = await _stockService.GetStockByIdAsync(productId);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpPost]
        public async Task<ActionResult> CreateStock([FromBody] StockDto stockDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var stock = new Stock
                {
                    SizeId = stockDto.Id,
                    UnitInStock = stockDto.UnitInStock
                };

                await _stockService.AddStockAsync(stock);

                return CreatedAtAction(nameof(GetStock), new { sizeId = stockDto.Id }, stockDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteStockByProductId(string productId)
        {
            try
            {
                await _stockService.DeleteStockAsync(productId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("quantity")]
        public async Task<ActionResult<StockQuantity>> QuantityInventoryMovement()
        {
            try
            {
                var res = await _stockService.QuantityInventoryMovement();

                if (res == null) return NotFound();

                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("quantitybyorderstatus")]
        public IActionResult GetProductQuantityByOrderStatus()
        {
            try
            {
                var productQuantity = _stockService.GetProductQuantityByOrderStatus();

                return Ok(productQuantity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("getmonthlyproduct")]
        public IActionResult GetMonthlyProduct()
        {
            try
            {
                var productMonthly = _stockService.GetSixMonthProductStats();

                return Ok(productMonthly);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("getquantityfortime")]
        public IActionResult GetQuantityForTime(DateTime startDate, DateTime endDate, Activity activity)
        {
            try
            {
                var quantity = _stockService.GetQuantityForTime(startDate, endDate, activity);

                return Ok(quantity);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
