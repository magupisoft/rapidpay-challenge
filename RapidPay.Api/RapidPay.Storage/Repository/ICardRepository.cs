using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;
using System.Threading.Tasks;

namespace RapidPay.Storage.Repository
{
    public interface ICardRepository
    {
        Task<CreateCardResponse> CreateNewCardAsync(CreateCardRequest request);

        Task<decimal?> GetCardBalance(string cardNumber);

        Task<bool> SaveCardPaymentTransaction(string cardNumber, decimal payment, decimal fee);

        Task<bool> UpdateBalance(string cardNumber, decimal amount);
    }
}