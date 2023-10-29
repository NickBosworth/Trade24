using Trade24.Common.Data;

namespace Trade24.Detection.Integration.Test
{
    public class EventDetectorsTests
    {
        [Fact]
        public void CanGetStates()
        {
            var context = new Context();

            var data = context.DailyStockData
                .Where(ds => ds.Symbol == "THG.L")
                .OrderBy(ds => ds.Date)
                .ToList();

            var richData = EnrichmentService.Enrich(data);

            var bullishStates = new List<DailyBullishStockIndicatorState>();
            var neutralStates = new List<DailyNeutralStockIndicatorState>();
            var bearishStates = new List<DailyBearishStockIndicatorState>();

            foreach (var item in richData)
            {
                bullishStates.Add(new DailyBullishStockIndicatorState() { Symbol = item.Symbol, Date = item.Date });
                neutralStates.Add(new DailyNeutralStockIndicatorState() { Symbol = item.Symbol, Date = item.Date });
                bearishStates.Add(new DailyBearishStockIndicatorState() { Symbol = item.Symbol, Date = item.Date });
            }

            StateDetector.UpdateState(richData, bullishStates, neutralStates, bearishStates);

            Assert.True(bullishStates.Count > 0);
            Assert.True(neutralStates.Count > 0);
            Assert.True(bearishStates.Count > 0);



        }


    }
}
