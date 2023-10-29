using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins
{
    public class BullishMorningStarPlugin : BasePlugin, IDetector
    {
        public BullishMorningStarPlugin()
        {
            Id = Guid.Parse("d95184eb-c9f7-4bb1-b267-4032a9bb0950");
            Name = "Bullish Morning Star";
            Description = "Bullish Morning Star is a bullish reversal candlestick pattern that consists of three candles. The first candle is a long bearish candle. The second candle is a small candle that gaps down. The third candle is a long bullish candle that gaps up and closes above the midpoint of the first candle.";
            MinimumDays = 30;
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

            // Considering the last three days for the Morning Star pattern.
            var day1 = data[data.Count - 3];
            var day2 = data[data.Count - 2];
            var day3 = data.Last();

            // Check if day1 is a long bearish candle (closed much lower than it opened).
            bool isDay1Bearish = day1.Close < day1.Open && (day1.Open - day1.Close) >= (day1.High - day1.Low) * 0.6M;

            // Check if day2 (the 'star') gapped down from day1 and has a short body.
            bool isDay2Star = day2.Open < day1.Close && (Math.Abs(day2.Close - day2.Open) <= (day2.High - day2.Low) * 0.3M);  // Assuming the body is less than 30% of its total range.

            // Check if day3 is a long bullish candle that gapped up from day2 and closed into day1's body.
            bool isDay3Bullish = day3.Open > day2.Close && day3.Close > day3.Open && (day3.Close - day3.Open) >= (day3.High - day3.Low) * 0.6M && day3.Close > (day1.Open + day1.Close) / 2;

            // Check if the majority of the days before these three were in a downtrend.
            int downtrendDays = 0;
            for (int i = 3; i < MinimumDays; i++)  // You may adjust the range depending on how many days you consider for the downtrend.
            {
                if (data[data.Count - 1 - i].Close < data[data.Count - 1 - i].Open)
                {
                    downtrendDays++;
                }
            }

            // Assuming that most of the previous days (excluding the last three) should be in a downtrend.
            LastDayIsMatch = isDay1Bearish && isDay2Star && isDay3Bullish && downtrendDays >= (MinimumDays - 3) / 2;
        }

    }
}
