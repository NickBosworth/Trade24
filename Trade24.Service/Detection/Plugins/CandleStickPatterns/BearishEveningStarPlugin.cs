using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins.CandleStickPatterns
{
    public class BearishEveningStarPlugin : BasePlugin, IDetector
    {
        public BearishEveningStarPlugin()
        {
            Id = Guid.Parse("ef555f57-ab5f-4059-b951-5712c7a78323");
            Name = "Bearish Evening Star";
            Description = "Bearish Evening Star";
            MinimumDays = 5;
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

            // Considering the last three days for the Evening Star pattern.
            var day1 = data[data.Count - 3];
            var day2 = data[data.Count - 2];
            var day3 = data.Last();

            // Check if the first day is a bullish day.
            bool isFirstDayBullish = day1.Close > day1.Open;

            // Check if the second day has a small body and gaps away from the first day.
            bool isSecondDayGapped = day2.Open > day1.High || day2.Close > day1.High;
            decimal bodySizeDay2 = Math.Abs(day2.Close - day2.Open);
            bool isSecondDaySmallBody = bodySizeDay2 < day1.Close - day1.Open;

            // Check if the third day is a bearish day that moves well into the body of the first candle.
            bool isThirdDayBearish = day3.Close < day3.Open;
            bool isThirdDayEngulfing = day3.Close < (day1.Open + day1.Close) / 2;

            // Checking if the preceding days were in an uptrend.
            int uptrendDays = 0;
            for (int i = 3; i < MinimumDays; i++)
            {
                if (data[data.Count - 1 - i].Close > data[data.Count - 1 - i].Open)
                {
                    uptrendDays++;
                }
            }

            // Assuming that most of the previous days (excluding the last three) should be in an uptrend.
            LastDayIsMatch = isFirstDayBullish && isSecondDayGapped && isSecondDaySmallBody && isThirdDayBearish && isThirdDayEngulfing && uptrendDays >= (MinimumDays - 3) / 2;
        }

    }
}
