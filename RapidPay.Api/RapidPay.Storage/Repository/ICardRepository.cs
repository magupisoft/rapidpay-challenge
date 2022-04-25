using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;
using System.Threading.Tasks;

namespace RapidPay.Storage.Repository
{
    public interface ICardRepository
    {
        Task<CreateCardResponse> CreateNewCardAsync(CreateCardRequest request);

        Task<decimal?> GetCardBalance(string cardNumber);

        Task<bool> CardPayment(string cardNumber, decimal payment);
    }
}