using Microsoft.Extensions.Logging;
using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;
using RapidPay.Storage.Repository;
using System.Threading.Tasks;

namespace RapidPay.CardManagement
{
    public class CardManagementService : ICardManagementService
    {
        private readonly ICardRepository _cardRepository;
        private readonly ILogger<CardManagementService> _logger;
        public CardManagementService(ICardRepository cardRepository, ILogger<CardManagementService> logger)
        {
            _cardRepository = cardRepository;
            _logger = logger;
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
