using Trade24.Service.Data;
using Trade24.Service.Detection;

namespace Trade24.Service.Test
{
    public class EventDetectorsTests
    {
        [Fact]
        public void CanGetStates()
        {
            var context = new SqliteContext();

            var data = context.DailyStockData
                .Where(ds => ds.Symbol == "THG.L")
                .OrderBy(ds => ds.Date)
                .ToList();

            var richData = EnrichmentService.Enrich(data);

            var states = new List<DailyStockIndicatorState>();


            foreach (var item in richData)
            {
                states.Add(new DailyStockIndicatorState() { Symbol = item.Symbol, Date = item.Date });
            }

            StateDetector.UpdateState(richData, states);

            Assert.True(states.Count > 0);



        }


    }
}
