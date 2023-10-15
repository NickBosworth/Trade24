using Trade24.Common.Data;

namespace Trade24.Detection.Integration.Test
{
    public class EventDetectorsTests
    {
        [Fact]
        public void CanGetSmaEvents()
        {
            var context = new Context();

            var data = context.DailyStockData
                .Where(ds => ds.Symbol == "THG.L")
                .OrderBy(ds => ds.Date)
                .ToList();

            var richData = EnrichmentService.Enrich(data);


            Assert.True(SMAEventDetectors.GetSma50Events(richData).Count > 0);
            Assert.True(SMAEventDetectors.GetSma200Events(richData).Count > 0);
            Assert.True(SMAEventDetectors.GetSma20Events(richData).Count > 0);
            Assert.True(SMAEventDetectors.GetSma10Events(richData).Count > 0);
            Assert.True(SMAEventDetectors.GetSma5Events(richData).Count > 0);
            Assert.True(SMAEventDetectors.GetSma9Events(richData).Count > 0);
            Assert.True(SMAEventDetectors.GetSma21Events(richData).Count > 0);
            Assert.True(SMAEventDetectors.GetSma100Events(richData).Count > 0);

        }

        [Fact]
        public void CanGetEmaEvents()
        {
            var context = new Context();

            var data = context.DailyStockData
                .Where(ds => ds.Symbol == "THG.L")
                .OrderBy(ds => ds.Date)
                .ToList();

            var richData = EnrichmentService.Enrich(data);


            Assert.True(EMAEventDetectors.GetEma12Events(richData).Count > 0);
            Assert.True(EMAEventDetectors.GetEma26Events(richData).Count > 0);
            Assert.True(EMAEventDetectors.GetEma50Events(richData).Count > 0);
            Assert.True(EMAEventDetectors.GetEma100Events(richData).Count > 0);
            Assert.True(EMAEventDetectors.GetEma200Events(richData).Count > 0);
            Assert.True(EMAEventDetectors.GetEma9Events(richData).Count > 0);
            Assert.True(EMAEventDetectors.GetEma21Events(richData).Count > 0);
            Assert.True(EMAEventDetectors.GetEma8Events(richData).Count > 0);
            Assert.True(EMAEventDetectors.GetEma5Events(richData).Count > 0);
            Assert.True(EMAEventDetectors.GetEma10Events(richData).Count > 0);
        }

        [Fact]
        public void CanGetRsiEvents()
        {
            var context = new Context();

            var data = context.DailyStockData
                .Where(ds => ds.Symbol == "THG.L")
                .OrderBy(ds => ds.Date)
                .ToList();

            var richData = EnrichmentService.Enrich(data);

            Assert.True(RSIEventDetectors.GetRsiEvents(richData).Count > 0);
        }

        [Fact]
        public void CanGetMacdEvents()
        {
            var context = new Context();

            var data = context.DailyStockData
                .Where(ds => ds.Symbol == "THG.L")
                .OrderBy(ds => ds.Date)
                .ToList();

            var richData = EnrichmentService.Enrich(data);

            Assert.True(MACDEventDetector.GetMacdEvents(richData).Count > 0);
        }

        [Fact]
        public void CanGetIchimokuEvents()
        {
            var context = new Context();

            var data = context.DailyStockData
                .Where(ds => ds.Symbol == "THG.L")
                .OrderBy(ds => ds.Date)
                .ToList();

            var richData = EnrichmentService.Enrich(data);

            Assert.True(IchimokuCloudEventDetectors.GetIchimokiCloudEvents(richData).Count > 0);
        }
    }
}
