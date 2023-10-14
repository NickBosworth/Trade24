using Microsoft.EntityFrameworkCore;
using Skender.Stock.Indicators;

namespace Trade24.Common.Data
{
    [PrimaryKey(nameof(Symbol), nameof(Date))]
    public class DailyStockData : IQuote
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }

        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal AdjustedClose { get; set; }
        public decimal Volume { get; set; }

    }
}
