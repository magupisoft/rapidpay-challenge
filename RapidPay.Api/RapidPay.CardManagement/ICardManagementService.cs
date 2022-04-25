using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;
using System.Threading.Tasks;

namespace RapidPay.CardManagement
{
    public interface ICardManagementService
    {
        public Task<CreateCardResponse> CreateNewCardAsync(CreateCardRequest request);
        public Task<CardBalanceResponse> GetCardBalance(string cardNumber);
    }
}
