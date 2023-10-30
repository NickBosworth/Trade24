using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins.CandleStickPatterns
{
    public class BearishHangingManPlugin : BasePlugin, IDetector
    {
        public BearishHangingManPlugin()
        {
            Id = Guid.Parse("74185a24-afd6-417d-832b-50ec9ec7d921");
            Name = "Bearish Hanging Man";
            Description = "The Hanging Man pattern is a bearish reversal pattern that suggests the potential end of an uptrend and the beginning of a downtrend. The hanging man has the same shape as the hammer, but it forms at the end of an uptrend";
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

            // Considering the last day for the Hanging Man pattern.
            var lastDay = data.Last();

            decimal bodySize = Math.Abs(lastDay.Close - lastDay.Open);
            decimal lowerShadow = lastDay.Open - lastDay.Low;
            decimal upperShadow = lastDay.High - lastDay.Close;

            // Check if the body is at the upper end of the trading range.
            bool isBodyAtUpperEnd = lastDay.Close > lastDay.Open;

            // Check if the lower shadow is at least twice the body's size.
            bool isLowerShadowTwiceBody = lowerShadow >= 2 * bodySize;

            // Check if there's little to no upper shadow.
            bool hasNoUpperShadow = upperShadow <= bodySize * 0.1M;  // Assuming upper shadow should be less than or equal to 10% of the body.

            // Check if the majority of the last 3 days were in an uptrend.
            int uptrendDays = 0;
            for (int i = 2; i >= 0; i--)
            {
                if (data[data.Count - 1 - i].Close > data[data.Count - 1 - i].Open)
                {
                    uptrendDays++;
                }
            }

            LastDayIsMatch = isBodyAtUpperEnd && isLowerShadowTwiceBody && hasNoUpperShadow && uptrendDays >= 2;
        }

    }
}
