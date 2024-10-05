using System.ComponentModel.DataAnnotations;

namespace NK.Core.Business.Model.test
{
    public class CreateCheckoutSessionRequest
    {
        [Required]
        public string PriceId { get; set; } = string.Empty;
    }
}
