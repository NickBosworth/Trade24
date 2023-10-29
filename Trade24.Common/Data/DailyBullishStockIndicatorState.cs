using Microsoft.EntityFrameworkCore;

namespace Trade24.Common.Data
{
    [PrimaryKey(nameof(Symbol), nameof(Date))]
    public class DailyBullishStockIndicatorState
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }

        //SMA
        public bool Above50Sma { get; set; }
        public bool Above200Sma { get; set; }
        public bool Above20Sma { get; set; }
        public bool Above10Sma { get; set; }
        public bool Above5Sma { get; set; }
        public bool Above9Sma { get; set; }
        public bool Above21Sma { get; set; }
        public bool Above100Sma { get; set; }


        //EMA
        public bool Above12Ema { get; set; }
        public bool Above26Ema { get; set; }
        public bool Above50Ema { get; set; }
        public bool Above100Ema { get; set; }
        public bool Above200Ema { get; set; }
        public bool Above9Ema { get; set; }
        public bool Above21Ema { get; set; }
        public bool Above8Ema { get; set; }
        public bool Above5Ema { get; set; }
        public bool Above10Ema { get; set; }

        //RSI
        public bool Rsi14Oversold { get; set; }
        public bool RsiBullishDivergence { get; set; }

        //MACD
        public bool MacdBullishCrossover { get; set; }
        public bool MacdBullishHistogram { get; set; }
        public bool AboveConversionLine { get; set; }
        public bool AboveBaseLine { get; set; }
        public bool AboveLeadingSpanA { get; set; }
        public bool AboveLeadingSpanB { get; set; }
        public bool AboveParabolicSAR { get; set; }
        public bool AboveStochasticOscillator { get; set; }
        public bool StochasticOscillatorBullishCross { get; set; }
        public bool OBVIncreasing { get; set; }
    }
}
