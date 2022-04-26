using Microsoft.EntityFrameworkCore;
using RapidPay.Storage.DbModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Storage.Repository
{
    public class PaymentFeeRepository : IPaymentFeeRepository
    {
        private readonly RapidPayContext _context;

        public PaymentFeeRepository(RapidPayContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateNewPaymentFee(decimal fee)
        {
            var paymentFee = new PaymentFee
            {
                Fee = fee,
                LastUpdated = DateTime.Now
            };
            _context.Add(paymentFee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            
            return true;
        }

        public async Task<(decimal currentFee, DateTime lastUpdated)> GetLastPaymentFee()
        {
            var paymentFee = await _context.PaymentFees
                                        .OrderBy(x => x.LastUpdated)
                                        .Take(1)
                                        .SingleOrDefaultAsync();
            if (paymentFee == null)
            {
                return default;
            }
            return (paymentFee.Fee, paymentFee.LastUpdated);
        }
    }
}
