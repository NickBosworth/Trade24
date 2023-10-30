using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins.CandleStickPatterns
{
    public class BullishEngulfingPlugin : BasePlugin, IDetector
    {
        public BullishEngulfingPlugin()
        {
            Id = Guid.Parse("d01928d1-bc99-40cc-b0c5-f7f5d251ab8c");
            Name = "Bullish Engulfing";
            Description = "Bullish Engulfing is a reversal candlestick pattern that occurs when a small bearish candle is followed by a large bullish candle that completely eclipses or 'engulfs' the previous day's candle.";
            MinimumDays = 10;
            SignalType = SignalType.Bullish;
        }

        public override void Detect(List<DailyStockData> data)
        {
            if (data == null || data.Count == 0 || data.Count < MinimumDays)
            {
                LastDayIsMatch = false;
                return;
            }

            data = data.OrderBy(d => d.Date).ToList();

            // Considering the last two days for the bullish engulfing pattern.
            var day1 = data[data.Count - 2];
            var day2 = data.Last();

            // Check if day1 (the first of the two candles) is a bearish candle (closed lower than it opened).
            bool isDay1Bearish = day1.Close < day1.Open;

            // Check if day2 (the second of the two candles) is a bullish candle (closed higher than it opened).
            bool isDay2Bullish = day2.Close > day2.Open;

            // Check if day2 engulfs day1.
            bool isDay2Engulfing = day2.Open < day1.Close && day2.Close > day1.Open;

            // Check if the majority of the previous days (excluding the last two) were in a downtrend.
            int downtrendDays = 0;
            for (int i = 2; i < MinimumDays; i++) // You may adjust the range depending on how many days you consider for the downtrend.
            {
                if (data[data.Count - 1 - i].Close < data[data.Count - 1 - i].Open)
                {
                    downtrendDays++;
                }
            }

            // Assuming at least half of the previous days (excluding the last two) should be in a downtrend.
            LastDayIsMatch = isDay1Bearish && isDay2Bullish && isDay2Engulfing && downtrendDays >= (MinimumDays - 2) / 2;
        }

    }
}
