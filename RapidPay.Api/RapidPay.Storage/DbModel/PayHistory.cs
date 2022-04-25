using System;

namespace RapidPay.Storage.DbModel
{
    public class PayHistory
    {
        public int Id { get; set; }
        public decimal Payment { get; set; }
        public decimal Fee { get; set; }
        public DateTime PayDate { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
