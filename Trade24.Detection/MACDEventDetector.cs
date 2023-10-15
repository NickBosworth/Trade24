using Trade24.Common.Data;

namespace Trade24.Detection
{
    public static class MACDEventDetector
    {
        public static List<Event> GetMacdEvents(List<RichDailyStockData> data)
        {
            var events = new List<Event>();

            data = data.Where(d => d.MACD != 0 && d.MACDSignal != 0).OrderBy(d => d.Date).ToList();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                //If the MACD line crosses the signal line from below log a bullish event.
                if (item.MACD > item.MACDSignal && data[data.IndexOf(item) - 1].MACD < data[data.IndexOf(item) - 1].MACDSignal)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("cedc3248-de95-42b0-8640-ef721359a45f"),
                        Description = "MACD closed above signal",
                        Symbol = item.Symbol,
                        Direction = Direction.Bullish
                    });
                }

                //If the MACD Histogram is above zero and greater than the previous day, log a bullish event.
                if (item.MACDHistogram > 0 && item.MACDHistogram > data[data.IndexOf(item) - 1].MACDHistogram)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("cdfa9042-9f02-47c1-b2af-cabdfd38f518"),
                        Description = "MACD Histogram is above zero and increasing",
                        Symbol = item.Symbol,
                        Direction = Direction.Bullish
                    });
                }

                //TODO: Positive Divergence. This one will be more involved.

                //If the MACD Fast EMA crosses the MACD Slow EMA from below log a bullish event.
                if (item.MACDFastEMA > item.MACDSlowEMA && data[data.IndexOf(item) - 1].MACDFastEMA < data[data.IndexOf(item) - 1].MACDSlowEMA)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("e416f5dd-06fd-45ef-a77c-503a0fcc31d6"),
                        Description = "MACD Fast EMA crossed MACD Slow EMA",
                        Symbol = item.Symbol,
                        Direction = Direction.Bullish
                    });
                }

                //If the MACD line crosses the signal line from above log a bearish event.
                if (item.MACD < item.MACDSignal && data[data.IndexOf(item) - 1].MACD > data[data.IndexOf(item) - 1].MACDSignal)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("64a04696-13b6-406d-b4b3-e19815f3ef0f"),
                        Description = "MACD closed below signal",
                        Symbol = item.Symbol,
                        Direction = Direction.Bearish
                    });
                }

                //If the MACD Histogram is below zero and less than the previous day, log a bearish event.
                if (item.MACDHistogram < 0 && item.MACDHistogram < data[data.IndexOf(item) - 1].MACDHistogram)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("36398c7d-3c29-4c8f-a44a-20eaf78da97c"),
                        Description = "MACD Histogram is below zero and decreasing",
                        Symbol = item.Symbol,
                        Direction = Direction.Bearish
                    });
                }

                //TODO: Negative Divergence. This one will be more involved.

                //If the MACD Fast EMA crosses the MACD Slow EMA from above log a bearish event.
                if (item.MACDFastEMA < item.MACDSlowEMA && data[data.IndexOf(item) - 1].MACDFastEMA > data[data.IndexOf(item) - 1].MACDSlowEMA)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("10c9ec23-7e0f-4d82-8d20-32c880a971b2"),
                        Description = "MACD Fast EMA crossed MACD Slow EMA",
                        Symbol = item.Symbol,
                        Direction = Direction.Bearish
                    });
                }


            }

            return events;
        }
    }
}
