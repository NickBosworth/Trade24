using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins.CandleStickPatterns
{
    public class BearishThreeBlackCrowsPlugin : BasePlugin, IDetector
    {
        public BearishThreeBlackCrowsPlugin()
        {
            Id = Guid.Parse("0abc1fd7-4f4c-45a7-98cf-14bfbb126003");
            Name = "Bearish Three Black Crows";
            Description = "Bearish Three Black Crows is a candlestick pattern that you can identify when a bearish market is about to reverse.";
            MinimumDays = 3;
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

            // Considering the last three days for the Three Black Crows pattern.
            var day1 = data[data.Count - 3];
            var day2 = data[data.Count - 2];
            var day3 = data.Last();

            // Check if all three days are bearish.
            bool isDay1Bearish = day1.Close < day1.Open;
            bool isDay2Bearish = day2.Close < day2.Open;
            bool isDay3Bearish = day3.Close < day3.Open;

            // Check if each day opened around the previous day's open but closed lower.
            bool isDay2SimilarOpen = day2.Open - day1.Open <= day1.Open * 0.01M; // Allowing 1% deviation in the open price
            bool isDay2ClosedLower = day2.Close < day1.Close;

            bool isDay3SimilarOpen = day3.Open - day2.Open <= day2.Open * 0.01M;
            bool isDay3ClosedLower = day3.Close < day2.Close;

            // Checking if the candles have short or non-existent wicks.
            decimal upperWickDay1 = day1.High - Math.Max(day1.Open, day1.Close);
            decimal lowerWickDay1 = Math.Min(day1.Open, day1.Close) - day1.Low;
            bool isDay1ShortWicks = upperWickDay1 <= (day1.Open - day1.Close) * 0.1M && lowerWickDay1 <= (day1.Open - day1.Close) * 0.1M;

            decimal upperWickDay2 = day2.High - Math.Max(day2.Open, day2.Close);
            decimal lowerWickDay2 = Math.Min(day2.Open, day2.Close) - day2.Low;
            bool isDay2ShortWicks = upperWickDay2 <= (day2.Open - day2.Close) * 0.1M && lowerWickDay2 <= (day2.Open - day2.Close) * 0.1M;

            decimal upperWickDay3 = day3.High - Math.Max(day3.Open, day3.Close);
            decimal lowerWickDay3 = Math.Min(day3.Open, day3.Close) - day3.Low;
            bool isDay3ShortWicks = upperWickDay3 <= (day3.Open - day3.Close) * 0.1M && lowerWickDay3 <= (day3.Open - day3.Close) * 0.1M;

            LastDayIsMatch = isDay1Bearish && isDay2Bearish && isDay3Bearish &&
                             isDay2SimilarOpen && isDay2ClosedLower &&
                             isDay3SimilarOpen && isDay3ClosedLower &&
                             isDay1ShortWicks && isDay2ShortWicks && isDay3ShortWicks;
        }

    }
}
