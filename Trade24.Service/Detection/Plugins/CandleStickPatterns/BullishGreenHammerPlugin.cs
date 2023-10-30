using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins.CandleStickPatterns
{
    public class BullishGreenHammerPlugin : BasePlugin, IDetector
    {
        public BullishGreenHammerPlugin()
        {
            Id = Guid.Parse("1275468c-ce7c-44f4-9391-d87e51cb4aad");
            Name = "Bullish Hammer (Green)";
            Description = "A bullish hammer is a candlestick chart pattern that occurs when a security trades lower than its opening, during the time period of the candle, but closes above or near its opening price.";
            MinimumDays = 30;
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

            var lastDay = data.Last();

            decimal bodySize = Math.Abs(lastDay.Close - lastDay.Open);
            decimal lowerShadow = lastDay.Open - lastDay.Low;
            decimal upperShadow = lastDay.High - lastDay.Close;

            // Check if the body is at the upper end of the trading range.
            bool isBodyAtUpperEnd = lastDay.Close > lastDay.Open;

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

            LastDayIsMatch = isBodyAtUpperEnd && isLowerShadowTwiceBody && hasNoUpperShadow && downtrendDays >= 2;

        }
    }
}
