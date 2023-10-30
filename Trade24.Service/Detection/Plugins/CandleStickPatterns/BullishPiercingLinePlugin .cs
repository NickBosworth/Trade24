using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins.CandleStickPatterns
{
    public class BullishPiercingLinePlugin : BasePlugin, IDetector
    {
        public BullishPiercingLinePlugin()
        {
            Id = Guid.Parse("8f397f23-0e32-40ae-b58a-af2c67fe8f7c");
            Name = "Bullish Piercing Line";
            Description = "Bullish Piercing Line is a bullish reversal candlestick pattern that occurs in downtrends. The pattern consists of two candlesticks: the first is black and the second is white. The white candlestick opens below the low of the preceding black candlestick and closes within the body of the black candlestick, above its midpoint.";
            MinimumDays = 2;
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

            // Considering the last two days for the Piercing Line pattern.
            var day1 = data[data.Count - 2];
            var day2 = data.Last();

            // Check if day1 is a long bearish candle (closed much lower than it opened).
            bool isDay1Bearish = day1.Close < day1.Open && day1.Open - day1.Close >= (day1.High - day1.Low) * 0.6M;  // Assume a long candle has a body that is at least 60% of its total range.

            // Check if day2 is a long bullish candle (closed much higher than it opened).
            bool isDay2Bullish = day2.Close > day2.Open && day2.Close - day2.Open >= (day2.High - day2.Low) * 0.6M;

            // Check if there's a gap down between day1's close and day2's open.
            bool hasGapDown = day2.Open < day1.Close;

            // Check if day2's close is above the midpoint of day1's body but below its open.
            decimal day1Midpoint = day1.Open + (day1.Close - day1.Open) / 2;
            bool closesAboveMidpoint = day2.Close > day1Midpoint && day2.Close < day1.Open;

            // Check if the majority of the previous days (excluding the last two) were in a downtrend.
            int downtrendDays = 0;
            for (int i = 2; i < MinimumDays; i++)  // You may adjust the range depending on how many days you consider for the downtrend.
            {
                if (data[data.Count - 1 - i].Close < data[data.Count - 1 - i].Open)
                {
                    downtrendDays++;
                }
            }

            // Assuming at least half of the previous days (excluding the last two) should be in a downtrend.
            LastDayIsMatch = isDay1Bearish && isDay2Bullish && hasGapDown && closesAboveMidpoint && downtrendDays >= (MinimumDays - 2) / 2;
        }

    }
}
