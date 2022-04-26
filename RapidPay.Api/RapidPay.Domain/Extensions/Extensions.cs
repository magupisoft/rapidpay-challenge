using System;

namespace RapidPay.Domain.Extensions
{
    public static class Extensions
    {
        public static decimal NextDecimal(this Random rnd, double minValue, double maxValue)
        {
            return (decimal)(rnd.NextDouble() * (maxValue - minValue));
        }
    }
}
