using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NK.Core.Business.Model.Order;
using NK.Core.Business.Model.test;
using NK.Core.Model;
using Stripe;
using Stripe.Checkout;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public PaymentController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            StripeConfiguration.ApiKey = "sk_test_51PED7RDLYy9bkcFned84YRMu1fU31Idjui8frjY11zGzl8dIlIXSOlS9sSdp3Fa7KThZhM8btdnt9p6cGHbFRNqg00qdaNup5b";
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOutStripe([FromBody] OrderDetailDto listOrderDetail)
        {
            var domain = "http://localhost:4200/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "customer/success",
                CancelUrl = domain + "customer/purchase",
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            }; 

            foreach (var item in listOrderDetail.Orders)
            {

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
            try
            {
                Session session = await service.CreateAsync(options);
                return Ok(new CreateCheckoutSessionResponse
                {
                    SessionId = session.Id,
                });
            }
            catch (StripeException e)
            {
                Console.WriteLine(e.StripeError.Message);
                return BadRequest(new ErrorResponse
                {
                    ErrorMessage = new ErrorMessage
                    {
                        Message = e.StripeError.Message
                    }
                });
            }
            //Session session = service.Create(options);

            //Response.Headers.Add("Location", session.Url);

            //return Ok(new { session = session.Url });
        }
    }
}
