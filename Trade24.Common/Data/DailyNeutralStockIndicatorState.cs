using Microsoft.EntityFrameworkCore;

namespace Trade24.Common.Data
{
    [PrimaryKey(nameof(Symbol), nameof(Date))]
    public class DailyNeutralStockIndicatorState
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }

        //ADX
        public bool AdxWeak_20_25 { get; set; }
        public bool AdxModerate_25_30 { get; set; }
        public bool AdxStrong_30_40 { get; set; }
        public bool AdxVeryStrong_40_50 { get; set; }
        public bool AdxExtremelyStrong_50_N { get; set; }
    }
}
