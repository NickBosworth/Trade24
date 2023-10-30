using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins.CandleStickPatterns
{
    public class BearishDarkCloudCoverPlugin : BasePlugin, IDetector
    {
        public BearishDarkCloudCoverPlugin()
        {
            Id = Guid.Parse("669740db-0582-4428-8020-3b0d9fc36d96");
            Name = "Bearish Dark Cloud Cover";
            Description = "Bearish Dark Cloud Cover";
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

            // Considering the last two days for the Dark Cloud Cover pattern.
            var day1 = data[data.Count - 2];
            var day2 = data.Last();

            // Check if the first day is a bullish day.
            bool isFirstDayBullish = day1.Close > day1.Open;

            // Check if the second day opened above the first day's close and closed below its midpoint.
            bool isSecondDayOpenedAboveFirstDay = day2.Open > day1.Close;
            bool isSecondDayClosedBelowMidpoint = day2.Close < (day1.Open + day1.Close) / 2;

            // Checking if the candles have short wicks.
            decimal upperWickDay1 = day1.High - day1.Close;
            decimal lowerWickDay1 = day1.Open - day1.Low;
            bool isDay1ShortWicks = upperWickDay1 <= (day1.Close - day1.Open) * 0.1M && lowerWickDay1 <= (day1.Close - day1.Open) * 0.1M;

            decimal upperWickDay2 = day2.High - day2.Open;
            decimal lowerWickDay2 = day2.Close - day2.Low;
            bool isDay2ShortWicks = upperWickDay2 <= (day2.Open - day2.Close) * 0.1M && lowerWickDay2 <= (day2.Open - day2.Close) * 0.1M;

            LastDayIsMatch = isFirstDayBullish && isSecondDayOpenedAboveFirstDay && isSecondDayClosedBelowMidpoint && isDay1ShortWicks && isDay2ShortWicks;
        }


    }
}
