using System.ComponentModel.DataAnnotations;

namespace Trade24.Common.Data
{
    public class StockTicker
    {
        [Key]
        public string Symbol { get; set; }

        public bool HasHistory { get; set; }

        public bool IsErroneous { get; set; }
    }
}
