using System.ComponentModel.DataAnnotations;

namespace RapidPay.Domain.Requests
{
    public class CreateCardRequest
    {
        [Required]
        public string Number { get; set; }
        public decimal Balance { get; set; }
    }
}
