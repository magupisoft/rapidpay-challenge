namespace RapidPay.Domain.Responses
{
    public class CardPaymentResponse
    {
        public CardPaymentResponse(string cardNumber)
        {
            CardNumber = cardNumber;
        }
        public string CardNumber { get; set; }
        public decimal AmountPaid { get; set; }

        public decimal FeePaid { get; set; }
    }
}
