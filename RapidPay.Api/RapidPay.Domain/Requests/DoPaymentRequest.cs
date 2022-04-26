using System.ComponentModel.DataAnnotations;

namespace RapidPay.Domain.Requests
{
    public class DoPaymentRequest
    {
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
