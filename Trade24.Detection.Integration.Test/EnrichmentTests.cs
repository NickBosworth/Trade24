using Trade24.Common.Data;

namespace Trade24.Detection.Integration.Test
{
    public class EnrichmentTests
    {
        [Fact]
        public void DataIsEnriched()
        {
            var context = new Context();

            var data = context.DailyStockData
                .Where(ds => ds.Symbol == "THG.L")
                .OrderBy(ds => ds.Date)
                .ToList();

            //Enrich the data.
            var richData = EnrichmentService.Enrich(data);

            //Assert that the data is enriched.
            Assert.True(richData.Count > 0);
            Assert.Contains(richData, rd => rd.SMA50 > 0);
            Assert.Contains(richData, rd => rd.SMA200 > 0);
            Assert.Contains(richData, rd => rd.SMA20 > 0);
            Assert.Contains(richData, rd => rd.SMA10 > 0);
            Assert.Contains(richData, rd => rd.SMA5 > 0);
            Assert.Contains(richData, rd => rd.SMA9 > 0);
            Assert.Contains(richData, rd => rd.SMA21 > 0);
            Assert.Contains(richData, rd => rd.SMA100 > 0);

            //Assert that the data has RSI.
            Assert.Contains(richData, rd => rd.RSI14 > 0);

            //Assert that the data has MACD.
            Assert.Contains(richData, rd => rd.MACD > 0);
            Assert.Contains(richData, rd => rd.MACDSignal > 0);
            Assert.Contains(richData, rd => rd.MACDHistogram > 0);
            Assert.Contains(richData, rd => rd.MACDFastEMA > 0);

            //Assert that the data has Ichimoku Cloud.
            Assert.Contains(richData, rd => rd.IchimokuConversionLine > 0);
            Assert.Contains(richData, rd => rd.IchimokuBaseLine > 0);
            Assert.Contains(richData, rd => rd.IchimokuLeadingSpanA > 0);
            Assert.Contains(richData, rd => rd.IchimokuLeadingSpanB > 0);
            Assert.Contains(richData, rd => rd.IchimokuLaggingSpan > 0);



        }
    }
}
