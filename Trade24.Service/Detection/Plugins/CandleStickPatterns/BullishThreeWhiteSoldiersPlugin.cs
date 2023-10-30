using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins.CandleStickPatterns
{
    public class BullishThreeWhiteSoldiersPlugin : BasePlugin, IDetector
    {
        public BullishThreeWhiteSoldiersPlugin()
        {
            Id = Guid.Parse("12a05279-7abb-4209-a5b7-133b2b392741");
            Name = "Bullish Three White Soldiers";
            Description = "Bullish Three White Soldiers is a bullish candlestick pattern that is used to predict the reversal of the current downtrend.";
            MinimumDays = 5;
            SignalType = SignalType.StrongBullish;
        }

        public override void Detect(List<DailyStockData> data)
        {
            if (data == null || data.Count == 0 || data.Count < MinimumDays)
            {
                LastDayIsMatch = false;
                return;
            }

            data = data.OrderBy(d => d.Date).ToList();

            // Considering the last three days for the Three White Soldiers pattern.
            var day1 = data[data.Count - 3];
            var day2 = data[data.Count - 2];
            var day3 = data.Last();

            // Check if all three days are long bullish candles.
            bool areAllThreeBullish = day1.Close > day1.Open && day2.Close > day2.Open && day3.Close > day3.Open;

            // Check if each day opened within the body of the previous day.
            bool didDay2OpenInDay1Body = day2.Open > day1.Open && day2.Open < day1.Close;
            bool didDay3OpenInDay2Body = day3.Open > day2.Open && day3.Open < day2.Close;

            // Check if each day closed higher than the previous day.
            bool didDay2CloseHigher = day2.Close > day1.Close;
            bool didDay3CloseHigher = day3.Close > day2.Close;

            // Optionally, check if all three days had small wicks. For instance, we can check if the wicks are less than 20% of the entire candle's range.
            bool areWicksSmall = (day1.High - day1.Close) / (day1.High - day1.Low) < 0.2M && (day2.High - day2.Close) / (day2.High - day2.Low) < 0.2M && (day3.High - day3.Close) / (day3.High - day3.Low) < 0.2M;

            // Check if the majority of the days before these three were in a downtrend.
            int downtrendDays = 0;
            for (int i = 3; i < MinimumDays; i++)
            {
                if (data[data.Count - 1 - i].Close < data[data.Count - 1 - i].Open)
                {
                    downtrendDays++;
                }
            }

            // Assuming that most of the previous days (excluding the last three) should be in a downtrend.
            LastDayIsMatch = areAllThreeBullish && didDay2OpenInDay1Body && didDay3OpenInDay2Body && didDay2CloseHigher && didDay3CloseHigher && areWicksSmall && downtrendDays >= (MinimumDays - 3) / 2;
        }



    }
}
