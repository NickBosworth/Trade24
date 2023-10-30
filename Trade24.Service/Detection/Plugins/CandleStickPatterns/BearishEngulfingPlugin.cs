using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins.CandleStickPatterns
{
    public class BearishEngulfingPlugin : BasePlugin, IDetector
    {
        public BearishEngulfingPlugin()
        {
            Id = Guid.Parse("117f6d27-524e-4226-b284-623bed13e3ef");
            Name = "Bearish Engulfing";
            Description = "Bearish Engulfing is a reversal candlestick pattern that occurs when a small bearish candle is followed by a large bullish candle that completely eclipses or 'engulfs' the small one.";
            MinimumDays = 2;
            SignalType = SignalType.Bearish;
        }

        public override void Detect(List<DailyStockData> data)
        {
            if (data == null || data.Count == 0 || data.Count < MinimumDays)
            {
                LastDayIsMatch = false;
                return;
            }

            data = data.OrderBy(d => d.Date).ToList();

            // Considering the last two days for the Bearish Engulfing pattern.
            var day1 = data[data.Count - 2];
            var day2 = data.Last();

            // Check if the first day is a bullish day with a small body.
            bool isFirstDayBullish = day1.Close > day1.Open;

            // Check if the second day is a bearish day.
            bool isSecondDayBearish = day2.Close < day2.Open;

            // Check if the second day's body engulfs the first day's body.
            bool isSecondDayEngulfing = day2.Open > day1.Close && day2.Close < day1.Open;

            // Check if the majority of the days before these two were in an uptrend.
            int uptrendDays = 0;
            for (int i = 2; i < MinimumDays; i++)
            {
                if (data[data.Count - 1 - i].Close > data[data.Count - 1 - i].Open)
                {
                    uptrendDays++;
                }
            }

            // Assuming that most of the previous days (excluding the last two) should be in an uptrend.
            LastDayIsMatch = isFirstDayBullish && isSecondDayBearish && isSecondDayEngulfing && uptrendDays >= (MinimumDays - 2) / 2;
        }

    }
}
