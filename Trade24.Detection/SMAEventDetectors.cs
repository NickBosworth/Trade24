using Trade24.Common.Data;

namespace Trade24.Detection
{
    public class SMAEventDetectors
    {
        public List<Event> GetSma50Events(List<RichDailyStockData> data)
        {
            //filter the data to only those where 50 sma is not 0.
            data = data.Where(d => d.SMA50 != 0).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.SMA50 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].SMA50)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        Type = EventType.CloseCross50Sma
                    });
                }
                else if (item.Close < item.SMA50 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].SMA50)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        Type = EventType.CloseCross50Sma
                    });
                }


            }

            return events;
        }
    }
}
