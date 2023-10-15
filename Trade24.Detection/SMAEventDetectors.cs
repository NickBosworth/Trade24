using Trade24.Common.Data;

namespace Trade24.Detection
{
    public static class SMAEventDetectors
    {
        public static List<Event> GetSma50Events(List<RichDailyStockData> data)
        {
            //filter the data to only those where 50 sma is not 0.
            data = data.Where(d => d.SMA50 != null).ToList();

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
                        EventTypeId = Guid.Parse("4a930c52-f6b9-4b81-a0eb-ae812e4f5026"),
                        Description = "closed above the 50 SMA"
                    });
                }
                else if (item.Close < item.SMA50 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].SMA50)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("f974890c-b892-47d8-af32-f57102353c05"),
                        Description = "closed below the 50 SMA"
                    });
                }


            }

            return events;
        }

        public static List<Event> GetSma200Events(List<RichDailyStockData> data)
        {
            //filter the data to only those where 200 sma is not 0.
            data = data.Where(d => d.SMA200 != null).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.SMA200 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].SMA200)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("653d7047-21aa-43d4-b298-7e6e2224313b"),
                        Description = "closed above the 200 SMA"
                    });
                }
                else if (item.Close < item.SMA200 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].SMA200)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("c90df9e1-2a30-4a82-917c-9669876efe19"),
                        Description = "closed below the 200 SMA"
                    });
                }
            }

            return events;
        }

        public static List<Event> GetSma20Events(List<RichDailyStockData> data)
        {
            //filter the data to only those where 20 sma is not 0.
            data = data.Where(d => d.SMA20 != null).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.SMA20 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].SMA20)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("f79b3b05-7c81-45e2-8049-a2aaada5bdca"),
                        Description = "closed above the 20 SMA"
                    });
                }
                else if (item.Close < item.SMA20 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].SMA20)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("209fcada-0143-4f9c-bb16-f51616a38094"),
                        Description = "closed below the 20 SMA"
                    });
                }
            }

            return events;
        }

        public static List<Event> GetSma10Events(List<RichDailyStockData> data)
        {
            //filter the data to only those where 10 sma is not 0.
            data = data.Where(d => d.SMA10 != null).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.SMA10 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].SMA10)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("e97de76b-c0ff-4908-b9d4-06299fef8a88"),
                        Description = "closed above the 10 SMA"
                    });
                }
                else if (item.Close < item.SMA10 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].SMA10)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("55a801cf-9faa-4c43-a5db-54dbd745e18d"),
                        Description = "closed below the 10 SMA"
                    });
                }
            }

            return events;
        }

        public static List<Event> GetSma5Events(List<RichDailyStockData> data)
        {
            //filter the data to only those where 5 sma is not 0.
            data = data.Where(d => d.SMA5 != null).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.SMA5 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].SMA5)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("b24188e1-dcb4-4cc6-9397-40433f18a77d"),
                        Description = "closed above the 5 SMA"
                    });
                }
                else if (item.Close < item.SMA5 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].SMA5)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("9e0855c5-730b-4a31-a623-90c222776431"),
                        Description = "closed below the 5 SMA"
                    });
                }
            }

            return events;
        }

        public static List<Event> GetSma9Events(List<RichDailyStockData> data)
        {
            //filter the data to only those where 9 sma is not 0.
            data = data.Where(d => d.SMA9 != null).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.SMA9 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].SMA9)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("c41b3764-16b8-44ca-a060-ec48bbf37bef"),
                        Description = "closed above the 9 SMA"
                    });
                }
                else if (item.Close < item.SMA9 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].SMA9)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("b9db0137-d11f-42d9-8e2b-3635a1fa01a1"),
                        Description = "closed below the 9 SMA"
                    });
                }
            }

            return events;
        }

        public static List<Event> GetSma21Events(List<RichDailyStockData> data)
        {
            //filter the data to only those where 21 sma is not 0.
            data = data.Where(d => d.SMA21 != null).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.SMA21 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].SMA21)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("99d52624-df9f-4eb6-8f3b-46acb8c9dafb"),
                        Description = "closed above the 21 SMA"
                    });
                }
                else if (item.Close < item.SMA21 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].SMA21)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("b4a215bf-a084-4705-92d1-6ad476fe6360"),
                        Description = "closed below the 21 SMA"
                    });
                }
            }

            return events;
        }

        public static List<Event> GetSma100Events(List<RichDailyStockData> data)
        {
            //filter the data to only those where 100 sma is not 0.
            data = data.Where(d => d.SMA100 != null).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.SMA100 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].SMA100)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("5de95621-bf76-46e1-8eee-50ed418ed26c"),
                        Description = "closed above the 100 SMA"
                    });
                }
                else if (item.Close < item.SMA100 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].SMA100)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("62a7855d-dce0-4c0b-ac88-a17ad395ad37"),
                        Description = "closed below the 100 SMA"
                    });
                }
            }

            return events;
        }

        //Handle the 9 and 21 sma cross events.
        public static List<Event> GetSma9Cross21Events(List<RichDailyStockData> data)
        {
            //filter the data to only those where 9 sma is not 0.
            data = data.Where(d => d.SMA21 != null && d.SMA9 != null).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First() || item == data.Last())
                {
                    continue;
                }

                if (item.SMA9 > item.SMA21 && data[data.IndexOf(item) - 1].SMA9 < data[data.IndexOf(item) - 1].SMA21)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("45c125b7-826a-4a4e-bcf3-465700bfea13"),
                        Description = "9 SMA crossed above the 21 SMA"
                    });
                }
                else if (item.SMA9 < item.SMA21 && data[data.IndexOf(item) - 1].SMA9 > data[data.IndexOf(item) - 1].SMA21)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("6a870567-9409-4f33-94c7-8a27ed332724"),
                        Description = "9 SMA crossed below the 21 SMA"
                    });
                }
            }

            return events;
        }
    }
}
