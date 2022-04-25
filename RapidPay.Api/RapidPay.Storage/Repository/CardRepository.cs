using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;
using RapidPay.Storage.DbModel;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Storage.Repository
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        private new readonly RapidPayContext _context;

        public CardRepository(RapidPayContext context) : base(context)
        {
            _context = context;
        }

        public Task<bool> CardPayment(string cardNumber, decimal payment)
        {
            throw new System.NotImplementedException();
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

        public async Task<decimal?> GetCardBalance(string cardNumber)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(x => x.Number == cardNumber);
            if (card == null)
            {
                return null;
            }
            return card.Balance;
        }
    }
}
