using Trade24.Common.Data;

namespace Trade24.Detection.Integration.Test
{
    public class BullFlagTests
    {
        [Fact]
        public void CanDetectBreakoutBreakDownBullFlags()
        {
            var context = new Context();

            var data = context.DailyStockData
                .Where(ds => ds.Symbol == "THG.L")
                .OrderBy(ds => ds.Date)
                .ToList();


        }
    }
}