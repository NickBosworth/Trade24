using Trade24.Common.Data;

namespace Trade24.Detection
{
    public static class RSIEventDetectors
    {
        public static List<Event> GetRsiEvents(List<RichDailyStockData> data)
        {
            data = data.Where(d => d.RSI14 != null).OrderBy(d => d.Date).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                //If RSI passed over 70 log a bullish event.
                if (item.RSI14 >= 70 && data[data.IndexOf(item) - 1].RSI14 < 70)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("093bdc31-16e2-4228-846d-54758c73b952"),
                        Description = "RSI closed over 70",
                        Symbol = item.Symbol,
                        Direction = Direction.Bullish
                    });
                }

                //If RSI passed under 30 log a bearish event.
                if (item.RSI14 <= 30 && data[data.IndexOf(item) - 1].RSI14 > 30)
                {
                    events.Add(new Event
                    {
                        Date = item.Date,
                        EventTypeId = Guid.Parse("6a0d3655-5f44-45e2-9592-eb2257ca72a8"),
                        Description = "RSI closed under 30",
                        Symbol = item.Symbol,
                        Direction = Direction.Bearish
                    });
                }
            }

            return events;
        }
    }
}
