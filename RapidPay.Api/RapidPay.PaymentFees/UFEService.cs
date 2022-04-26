using RapidPay.Storage.Repository;
using System;
using System.Threading.Tasks;
using RapidPay.Domain.Extensions;

namespace RapidPay.PaymentFees
{
    public sealed class UFEService
    {
        private static readonly Lazy<UFEService> lazy = new Lazy<UFEService>(() => new UFEService());

        public static UFEService Instance { get { return lazy.Value; } }

        private UFEService()
        {          
        }

        public async Task<decimal> GetPaymentFeeAsync(IPaymentFeeRepository repository)
        {
            var (currentFee, lastUpdated) = await repository.GetLastPaymentFee();
            decimal paymentFee = currentFee;
            if ((lastUpdated - DateTime.UtcNow).TotalHours > 1)
            {
                var newFee = new Random().NextDecimal(0.0, 2.0);
                if (await repository.CreateNewPaymentFee(newFee))
                {
                    paymentFee = newFee * (currentFee == default ? 1: currentFee);
                }
            }

            return paymentFee;
        }
    }
}
