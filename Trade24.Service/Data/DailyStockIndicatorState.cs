using Microsoft.EntityFrameworkCore;

namespace Trade24.Service.Data
{
    [PrimaryKey(nameof(Symbol), nameof(Date))]
    public class DailyStockIndicatorState
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }

        public bool ValidForTraining { get; set; }

        //SMA
        public bool BullishAbove50Sma { get; set; }
        public bool BullishAbove200Sma { get; set; }
        public bool BullishAbove20Sma { get; set; }
        public bool BullishAbove10Sma { get; set; }
        public bool BullishAbove5Sma { get; set; }
        public bool BullishAbove9Sma { get; set; }
        public bool BullishAbove21Sma { get; set; }
        public bool BullishAbove100Sma { get; set; }


        //EMA
        public bool BullishAbove12Ema { get; set; }
        public bool BullishAbove26Ema { get; set; }
        public bool BullishAbove50Ema { get; set; }
        public bool BullishAbove100Ema { get; set; }
        public bool BullishAbove200Ema { get; set; }
        public bool BullishAbove9Ema { get; set; }
        public bool BullishAbove21Ema { get; set; }
        public bool BullishAbove8Ema { get; set; }
        public bool BullishAbove5Ema { get; set; }
        public bool BullishAbove10Ema { get; set; }

        //RSI
        public bool BullishRsi14Oversold { get; set; }
        public bool BullishRsiBullishDivergence { get; set; }

        //MACD
        public bool BullishMacdBullishCrossover { get; set; }
        public bool BullishMacdBullishHistogram { get; set; }
        public bool BullishAboveConversionLine { get; set; }
        public bool BullishAboveBaseLine { get; set; }
        public bool BullishAboveLeadingSpanA { get; set; }
        public bool BullishAboveLeadingSpanB { get; set; }
        public bool BullishAboveParabolicSAR { get; set; }
        public bool BullishAboveStochasticOscillator { get; set; }
        public bool BullishStochasticOscillatorBullishCross { get; set; }
        public bool BullishOBVIncreasing { get; set; }

        //ADX
        public bool NeutralAdxWeak_20_25 { get; set; }
        public bool NeutralAdxModerate_25_30 { get; set; }
        public bool NeutralAdxStrong_30_40 { get; set; }
        public bool NeutralAdxVeryStrong_40_50 { get; set; }
        public bool NeutralAdxExtremelyStrong_50_N { get; set; }

        //SMA
        public bool BearishBelow50Sma { get; set; }
        public bool BearishBelow200Sma { get; set; }
        public bool BearishBelow20Sma { get; set; }
        public bool BearishBelow10Sma { get; set; }
        public bool BearishBelow5Sma { get; set; }
        public bool BearishBelow9Sma { get; set; }
        public bool BearishBelow21Sma { get; set; }
        public bool BearishBelow100Sma { get; set; }

        //EMA
        public bool BearishBelow12Ema { get; set; }
        public bool BearishBelow26Ema { get; set; }
        public bool BearishBelow50Ema { get; set; }
        public bool BearishBelow100Ema { get; set; }
        public bool BearishBelow200Ema { get; set; }
        public bool BearishBelow9Ema { get; set; }
        public bool BearishBelow21Ema { get; set; }
        public bool BearishBelow8Ema { get; set; }
        public bool BearishBelow5Ema { get; set; }
        public bool BearishBelow10Ema { get; set; }

        //RSI
        public bool BearishRsi14Oversold { get; set; }
        public bool BearishRsi14Overbought { get; set; }
        public bool BearishMacdBearishCrossover { get; set; }
        public bool BearishMacdBearishHistogram { get; set; }
        public bool BearishBelowConversionLine { get; set; }
        public bool BearishBelowBaseLine { get; set; }
        public bool BearishBelowLeadingSpanA { get; set; }
        public bool BearishBelowLeadingSpanB { get; set; }
        public bool BearishBelowParabolicSAR { get; set; }
        public bool BearishBelowStochasticOscillator { get; set; }
        public bool BearishStochasticOscillatorBearishCross { get; set; }
        public bool BearishOBVDecreasing { get; set; }

        //Results
        public decimal WeekChange { get; set; }
        public decimal MonthChange { get; set; }
        public decimal QuarterChange { get; set; }
        public decimal HalfYearChange { get; set; }
    }
}
