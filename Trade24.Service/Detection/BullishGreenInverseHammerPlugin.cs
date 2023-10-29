using Trade24.Service.Data;
using Trade24.Service.Detection.Plugins;

namespace Trade24.Service.Detection
{
    public class BullishGreenInverseHammerPlugin : BasePlugin, IDetector
    {
        public BullishGreenInverseHammerPlugin()
        {
            Id = Guid.Parse("d6e87964-aee6-434e-a423-563657c30c5c");
            Name = "Bullish Green Inverse Hammer";
            Description = "Bullish Green Inverse Hammer";
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

            var lastDay = data.Last();

            decimal bodySize = Math.Abs(lastDay.Close - lastDay.Open);
            decimal lowerShadow = lastDay.Open - lastDay.Low;
            decimal upperShadow = lastDay.High - lastDay.Close;

            // Check if the body is at the lower end of the trading range.
            bool isBodyAtLowerEnd = lastDay.Close < lastDay.Open;

            // Check if the upper shadow is at least twice the body's size.
            bool isUpperShadowTwiceBody = upperShadow >= 2 * bodySize;

            // Check if there's little to no lower shadow.
            bool hasNoLowerShadow = lowerShadow <= bodySize * 0.1M; // Assuming lower shadow should be less than or equal to 10% of the body.

            // Check if the majority of the last 3 days were in a downtrend.
            int downtrendDays = 0;
            for (int i = 2; i >= 0; i--)
            {
                if (data[data.Count - 1 - i].Close < data[data.Count - 1 - i].Open)
                {
                    downtrendDays++;
                }
            }

            LastDayIsMatch = isBodyAtLowerEnd && isUpperShadowTwiceBody && hasNoLowerShadow && downtrendDays >= 2;
        }

    }
}
