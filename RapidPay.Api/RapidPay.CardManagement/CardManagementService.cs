using Microsoft.Extensions.Logging;
using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;
using RapidPay.PaymentFees;
using RapidPay.Storage.Repository;
using System.Threading.Tasks;

namespace RapidPay.CardManagement
{
    public class CardManagementService : ICardManagementService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IPaymentFeeRepository _paymentFeeRepository;
        private readonly ILogger<CardManagementService> _logger;
      
        public CardManagementService(
            ICardRepository cardRepository,
            IPaymentFeeRepository paymentFeeRepository,
            ILogger<CardManagementService> logger
        )
        {
            _cardRepository = cardRepository;
            _logger = logger;
            _paymentFeeRepository = paymentFeeRepository;
        }

        public async Task<CardPaymentResponse> ProcessPayment(DoPaymentRequest request)
        {
            _logger.LogInformation(
                $"Process a card payment card with number {request.CardNumber} and amount {request.Amount}");
            var currentBalance = await GetCardBalance(request.CardNumber);
            if(currentBalance == null)
            {
                throw new System.Exception("There's no balance in the card");
            }           
            
            var feeToPay = await UFEService.Instance.GetPaymentFeeAsync(_paymentFeeRepository);

            var totalToBeDiscounted = request.Amount + feeToPay;
            if(currentBalance.Balance - totalToBeDiscounted < 0)
            {
                throw new System.Exception("There's not enough balance  to process this payment");
            }

            var response = new CardPaymentResponse(request.CardNumber);
            if (await _cardRepository.SaveCardPaymentTransaction(request.CardNumber, request.Amount, feeToPay))
            {
                await _cardRepository.UpdateBalance(request.CardNumber, request.Amount + feeToPay);
                response.AmountPaid = request.Amount;
                response.FeePaid = feeToPay;
            }

            return response;
        }

        public async Task<CreateCardResponse> CreateNewCardAsync(CreateCardRequest request)
        {
            _logger.LogInformation($"Create new card with number {request.Number}");
            return await _cardRepository.CreateNewCardAsync(request);
        }

        public async Task<CardBalanceResponse> GetCardBalance(string cardNumber)
        {
            decimal? balance = await _cardRepository.GetCardBalance(cardNumber);
            if (!balance.HasValue)
            {
                return null;
            }

            return new CardBalanceResponse { Balance = balance.Value, CardNumber = cardNumber };
        }
    }
}
