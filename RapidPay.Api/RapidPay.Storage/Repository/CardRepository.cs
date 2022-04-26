using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;
using RapidPay.Storage.DbModel;
using System.Threading.Tasks;

namespace RapidPay.Storage.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly RapidPayContext _context;

        public CardRepository(RapidPayContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveCardPaymentTransaction(string cardNumber, decimal payment, decimal fee)
        {
            var card = await GetCard(cardNumber);
            if (card == null)
            {
                return false;
            }
            var payTransaction = new PayHistory
            {
                Payment = payment,
                Fee = fee,
                PayDate = System.DateTime.Now,
                CardId = card.Id,
            };
            _context.Add(payTransaction);
            await _context.SaveChangesAsync();
            return true; ;
        }

        public async Task<CreateCardResponse> CreateNewCardAsync(CreateCardRequest request)
        {
            var newCard = new Card
            {
                Number = request.Number,
                Balance = request.Balance
            };
            _context.Add(newCard);
            await _context.SaveChangesAsync();
            return new CreateCardResponse() { CardId = newCard.Id, CardNumber = newCard.Number };
        }

        public async Task<bool> UpdateBalance(string cardNumber, decimal amount)
        {
            var card = await GetCard(cardNumber);
            if (card == null)
            {
                return false;
            }
            card.Balance -= amount;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal?> GetCardBalance(string cardNumber)
        {
            var card = await GetCard(cardNumber);
            if (card == null)
            {
                return null;
            }
            return card.Balance;
        }

        private async Task<Card> GetCard(string cardNumber)
        {
            return await _context.Cards.FirstOrDefaultAsync(x => x.Number == cardNumber);
        }
    }
}
