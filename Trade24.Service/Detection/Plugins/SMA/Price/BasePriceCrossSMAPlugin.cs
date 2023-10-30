using Skender.Stock.Indicators;
using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins.SMA.Price
{

    public abstract class BasePriceCrossSMAPlugin : BasePlugin, IDetector
    {
        public int Length { get; set; }
        public bool BullishCheck { get; set; }

        public override void Detect(List<DailyStockData> data)
        {
            if (data.Count < Length)
            {
                LastDayIsMatch = false;
                return;
            }

            data = data.OrderBy(x => x.Date).ToList();
            var smaResults = data.GetSma(Length).ToList();

            if (BullishCheck)
            {
                //Check if the second to last price closed below the SMA for the same date and the last price closed above the SMA for the same date.
                LastDayIsMatch = data[data.Count - 2].Close < Convert.ToDecimal(smaResults[smaResults.Count - 2].Sma) && data[data.Count - 1].Close > Convert.ToDecimal(smaResults[smaResults.Count - 1].Sma);
            }

            else
            {
                //Check if the second to last price closed above the SMA for the same date and the last price closed below the SMA for the same date.
                LastDayIsMatch = data[data.Count - 2].Close > Convert.ToDecimal(smaResults[smaResults.Count - 2].Sma) && data[data.Count - 1].Close < Convert.ToDecimal(smaResults[smaResults.Count - 1].Sma);
            }
        }
    }
}
