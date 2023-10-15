using Trade24.Common.Data;

namespace Trade24.Detection
{
    public static class IchimokuCloudEventDetectors
    {
        public static List<Event> GetIchimokiCloudEvents(List<RichDailyStockData> data)
        {
            var events = new List<Event>();

            data = data.Where(d => d.IchimokuConversionLine != null && d.IchimokuBaseLine != null && d.IchimokuLeadingSpanA != null && d.IchimokuLeadingSpanB != null && d.IchimokuLaggingSpan != null).OrderBy(d => d.Date).ToList();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                //If the conversion line crosses the base line from below log a bullish event.
                if (item.IchimokuConversionLine > item.IchimokuBaseLine && data[data.IndexOf(item) - 1].IchimokuConversionLine < data[data.IndexOf(item) - 1].IchimokuBaseLine)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("2f8553ee-ec82-4ef8-a07c-5ac3a596ea68"),
                        Description = "Ichimoku Cloud conversion line crossed base line",
                        Symbol = item.Symbol,
                        Direction = Direction.Bullish
                    });
                }

                //If the closing price is above the cloud and the cloud is green log a bullish event.
                if (item.Close > item.IchimokuLeadingSpanA && item.Close > item.IchimokuLeadingSpanB && item.IchimokuLeadingSpanA > item.IchimokuLeadingSpanB)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("67d9e82a-87e0-47a5-b7b7-a64beecb544c"),
                        Description = "Ichimoku Cloud is bullish",
                        Symbol = item.Symbol,
                        Direction = Direction.Bullish
                    });
                }

                //If the Chinkou (lagging) span is above the price, log a bullish event.
                if (item.IchimokuLaggingSpan > item.Close)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("f634ddd7-9870-4cd3-b325-1f3c08a54b51"),
                        Description = "Ichimoku Cloud lagging span is above price",
                        Symbol = item.Symbol,
                        Direction = Direction.Bullish
                    });
                }

                //If Leading Span A passes above Leading Span B log a bullish event.
                if (item.IchimokuLeadingSpanA > item.IchimokuLeadingSpanB && data[data.IndexOf(item) - 1].IchimokuLeadingSpanA < data[data.IndexOf(item) - 1].IchimokuLeadingSpanB)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("20e5fc4f-7ee1-4d03-9780-90944867b80c"),
                        Description = "Ichimoku Cloud Leading Span A crossed above Leading Span B",
                        Symbol = item.Symbol,
                        Direction = Direction.Bullish
                    });
                }

                //If the open price was below the cloud and it closed above, log a bullish event.
                if (item.Open < item.IchimokuLeadingSpanA && item.Open < item.IchimokuLeadingSpanB && item.IchimokuLeadingSpanA < item.IchimokuLeadingSpanB && item.Close > item.IchimokuLeadingSpanA && item.Close > item.IchimokuLeadingSpanB && item.IchimokuLeadingSpanA > item.IchimokuLeadingSpanB)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("ad7a581a-d412-44fd-b75e-09ab789e194c"),
                        Description = "Open was below the Ichimoku Cloud and closed above",
                        Symbol = item.Symbol,
                        Direction = Direction.Bullish
                    });
                }

                //If the conversion line crosses the base line from above log a bearish event.
                if (item.IchimokuConversionLine < item.IchimokuBaseLine && data[data.IndexOf(item) - 1].IchimokuConversionLine > data[data.IndexOf(item) - 1].IchimokuBaseLine)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("943575f4-66c2-4959-a990-0e7dd6d1d46b"),
                        Description = "Ichimoku Cloud conversion line crossed base line",
                        Symbol = item.Symbol,
                        Direction = Direction.Bearish
                    });
                }

                //If the closing price is below the cloud and the cloud is red log a bearish event.
                if (item.Close < item.IchimokuLeadingSpanA && item.Close < item.IchimokuLeadingSpanB && item.IchimokuLeadingSpanA < item.IchimokuLeadingSpanB)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("553ff3be-9711-4d6c-9768-7e6a259a74b2"),
                        Description = "Ichimoku Cloud is bearish",
                        Symbol = item.Symbol,
                        Direction = Direction.Bearish
                    });
                }

                //If the Chinkou (lagging) span is below the price, log a bearish event.
                if (item.IchimokuLaggingSpan < item.Close)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("192b1d4d-faa4-4bfa-8aaa-0791d8a88ff1"),
                        Description = "Ichimoku Cloud lagging span is below price",
                        Symbol = item.Symbol,
                        Direction = Direction.Bearish
                    });
                }

                //If Leading Span A passes below Leading Span B log a bearish event.
                if (item.IchimokuLeadingSpanA < item.IchimokuLeadingSpanB && data[data.IndexOf(item) - 1].IchimokuLeadingSpanA > data[data.IndexOf(item) - 1].IchimokuLeadingSpanB)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("93c23ef6-43a7-48a1-9935-1e96e07fdd28"),
                        Description = "Ichimoku Cloud Leading Span A crossed below Leading Span B",
                        Symbol = item.Symbol,
                        Direction = Direction.Bearish
                    });
                }

                //If the open price was above the cloud and it closed below, log a bearish event.
                if (item.Open > item.IchimokuLeadingSpanA && item.Open > item.IchimokuLeadingSpanB && item.IchimokuLeadingSpanA > item.IchimokuLeadingSpanB && item.Close < item.IchimokuLeadingSpanA && item.Close < item.IchimokuLeadingSpanB && item.IchimokuLeadingSpanA < item.IchimokuLeadingSpanB)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("10da2b2a-c1de-4c68-b224-0f3eb41129c3"),
                        Description = "Open was above the Ichimoku Cloud and closed below",
                        Symbol = item.Symbol,
                        Direction = Direction.Bearish
                    });
                }


            }



            return events;
        }
    }
}
