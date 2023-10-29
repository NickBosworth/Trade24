using Microsoft.EntityFrameworkCore;

namespace Trade24.Common.Data
{
    [PrimaryKey(nameof(Symbol), nameof(Date))]
    public class DailyBearishStockIndicatorState
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }

        //SMA
        public bool Below50Sma { get; set; }
        public bool Below200Sma { get; set; }
        public bool Below20Sma { get; set; }
        public bool Below10Sma { get; set; }
        public bool Below5Sma { get; set; }
        public bool Below9Sma { get; set; }
        public bool Below21Sma { get; set; }
        public bool Below100Sma { get; set; }

        //EMA
        public bool Below12Ema { get; set; }
        public bool Below26Ema { get; set; }
        public bool Below50Ema { get; set; }
        public bool Below100Ema { get; set; }
        public bool Below200Ema { get; set; }
        public bool Below9Ema { get; set; }
        public bool Below21Ema { get; set; }
        public bool Below8Ema { get; set; }
        public bool Below5Ema { get; set; }
        public bool Below10Ema { get; set; }

        //RSI
        public bool Rsi14Oversold { get; set; }
        public bool Rsi14Overbought { get; set; }
        public bool MacdBearishCrossover { get; set; }
        public bool MacdBearishHistogram { get; set; }
        public bool BelowConversionLine { get; set; }
        public bool BelowBaseLine { get; set; }
        public bool BelowLeadingSpanA { get; set; }
        public bool BelowLeadingSpanB { get; set; }
        public bool BelowParabolicSAR { get; set; }
        public bool BelowStochasticOscillator { get; set; }
        public bool StochasticOscillatorBearishCross { get; set; }
        public bool OBVDecreasing { get; set; }
    }
}
