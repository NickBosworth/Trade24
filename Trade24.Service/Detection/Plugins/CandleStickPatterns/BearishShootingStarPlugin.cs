using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins.CandleStickPatterns
{
    public class BearishShootingStarPlugin : BasePlugin, IDetector
    {
        public BearishShootingStarPlugin()
        {
            Id = Guid.Parse("b2a5d0a1-ec38-4ec9-b780-10cf5e00a578");
            Name = "Bearish Shooting Star";
            Description = "The Shooting Star pattern is a bearish reversal pattern, often appearing at the top of an uptrend";
            MinimumDays = 10;
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

            // Considering the last day for the Shooting Star pattern.
            var lastDay = data.Last();

            decimal bodySize = Math.Abs(lastDay.Close - lastDay.Open);
            decimal upperShadow = lastDay.High - Math.Max(lastDay.Close, lastDay.Open);
            decimal lowerShadow = Math.Min(lastDay.Close, lastDay.Open) - lastDay.Low;

            // Check if the body is near the lower end of the trading range.
            bool isBodyNearLowerEnd = lastDay.Close < lastDay.Open;

            // Check if the upper shadow is at least twice the body's size.
            bool isUpperShadowTwiceBody = upperShadow >= 2 * bodySize;

            // Check if there's little to no lower shadow.
            bool hasNoLowerShadow = lowerShadow <= bodySize * 0.1M;  // Assuming lower shadow should be less than or equal to 10% of the body.

            // Check if the majority of the last 3 days were in an uptrend.
            int uptrendDays = 0;
            for (int i = 2; i >= 0; i--)
            {
                if (data[data.Count - 1 - i].Close > data[data.Count - 1 - i].Open)
                {
                    uptrendDays++;
                }
            }

            LastDayIsMatch = isBodyNearLowerEnd && isUpperShadowTwiceBody && hasNoLowerShadow && uptrendDays >= 2;
        }

    }
}
