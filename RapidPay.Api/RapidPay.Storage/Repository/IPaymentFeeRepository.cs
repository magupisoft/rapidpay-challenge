using System;
using System.Threading.Tasks;

namespace RapidPay.Storage.Repository
{
    public interface IPaymentFeeRepository
    {
        Task<bool> CreateNewPaymentFee(decimal fee);

        Task<(decimal currentFee, DateTime lastUpdated)> GetLastPaymentFee();
    }
}