using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.Storage.DbModel
{
    public class Card
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public ICollection<PayHistory> PayHistories { get; set; }
    }
}
