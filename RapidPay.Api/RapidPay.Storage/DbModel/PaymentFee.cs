using System;

namespace RapidPay.Storage.DbModel
{
    public class PaymentFee
    {
        public int Id { get; set; }
        public decimal Fee { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
