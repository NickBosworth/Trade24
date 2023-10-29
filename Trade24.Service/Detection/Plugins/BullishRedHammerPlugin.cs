using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins
{
    public class BullishRedHammerPlugin : BasePlugin, IDetector
    {
        public BullishRedHammerPlugin()
        {
            Id = Guid.Parse("21645226-e37a-44f5-bbac-d396b5afa174");
            Name = "Bullish Hammer (Red)";
            Description = "A bullish hammer (Red) is a candlestick chart pattern that occurs when a security trades lower than its opening, during the time period of the candle, but closes below or near its opening price.";
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

            var lastDay = data.Last();

            decimal bodySize = Math.Abs(lastDay.Close - lastDay.Open);
            decimal lowerShadow = lastDay.Open - lastDay.Low;
            decimal upperShadow = lastDay.High - lastDay.Close;

            // Check if the body is at the upper end of the trading range.
            bool isBodyAtLowerEnd = lastDay.Close < lastDay.Open;

            // Check if the lower shadow is at least twice the body's size.
            bool isLowerShadowTwiceBody = lowerShadow >= 2 * bodySize;

            // Check if there's little to no upper shadow.
            bool hasNoUpperShadow = upperShadow <= bodySize * 0.1M; // Assuming upper shadow should be less than or equal to 10% of the body.

            // Check if the majority of the last 3 days were in a downtrend.
            int downtrendDays = 0;
            for (int i = 2; i >= 0; i--)
            {
                if (data[data.Count - 1 - i].Close < data[data.Count - 1 - i].Open)
                {
                    downtrendDays++;
                }
            }

            LastDayIsMatch = isBodyAtLowerEnd && isLowerShadowTwiceBody && hasNoUpperShadow && downtrendDays >= 2;

        }
    }
}

