using Trade24.Common.Data;

namespace Trade24.Detection
{
    public static class EMAEventDetectors
    {
        //Handle EMA12 Events.
        public static List<Event> GetEma12Events(List<RichDailyStockData> data)
        {
            data = data.Where(d => d.EMA12 != null).OrderBy(d => d.Date).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.EMA12 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].EMA12)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("a7a7bcf9-1394-4c61-a69f-bf5cea2ac47f"),
                        Description = "closed above the 12 EMA"
                    });
                }
                else if (item.Close < item.EMA12 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].EMA12)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("75066245-c09e-4b5d-b7b3-79d8eca80a06"),
                        Description = "closed below the 12 EMA"
                    });
                }


            }

            return events;
        }

        //Handle EMA26 Events.
        public static List<Event> GetEma26Events(List<RichDailyStockData> data)
        {
            data = data.Where(d => d.EMA26 != null).OrderBy(d => d.Date).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.EMA26 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].EMA26)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("939b6b77-b847-4393-aed0-10342ca72906"),
                        Description = "closed above the 26 EMA"
                    });
                }
                else if (item.Close < item.EMA26 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].EMA26)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("7c1a3f50-6f6e-4710-9797-8dfdf7cdd2d1"),
                        Description = "closed below the 26 EMA"
                    });
                }

            }

            return events;
        }

        //Handle EMA50 Events.
        public static List<Event> GetEma50Events(List<RichDailyStockData> data)
        {
            data = data.Where(d => d.EMA50 != null).OrderBy(d => d.Date).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.EMA50 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].EMA50)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("b1594358-7037-428a-b522-02b28fa26290"),
                        Description = "closed above the 50 EMA"
                    });
                }
                else if (item.Close < item.EMA50 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].EMA50)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("bab77480-ae9f-4187-b87e-a64b44ecdd79"),
                        Description = "closed below the 50 EMA"
                    });
                }

            }

            return events;
        }

        //Handle EMA100 Events.
        public static List<Event> GetEma100Events(List<RichDailyStockData> data)
        {
            data = data.Where(d => d.EMA100 != null).OrderBy(d => d.Date).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.EMA100 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].EMA100)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("8c996813-0f80-4a18-b21d-8a7fa6229f88"),
                        Description = "closed above the 100 EMA"
                    });
                }
                else if (item.Close < item.EMA100 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].EMA100)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("4e200e0f-dfb3-4770-aa15-5298ab26d5c7"),
                        Description = "closed below the 100 EMA"
                    });
                }

            }

            return events;
        }

        //Handle EMA200 Events.
        public static List<Event> GetEma200Events(List<RichDailyStockData> data)
        {
            data = data.Where(d => d.EMA200 != 0).OrderBy(d => d.Date).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.EMA200 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].EMA200)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("bf88f331-ef06-4f5d-a493-20a9af388b48"),
                        Description = "closed above the 200 EMA"
                    });
                }
                else if (item.Close < item.EMA200 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].EMA200)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("2df459d8-3b8e-4488-a1e4-bf69646b3321"),
                        Description = "closed below the 200 EMA"
                    });
                }

            }

            return events;
        }

        //Handle EMA9 Events.
        public static List<Event> GetEma9Events(List<RichDailyStockData> data)
        {
            data = data.Where(d => d.EMA9 != 0).OrderBy(d => d.Date).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.EMA9 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].EMA9)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("7a7d8faf-7395-4e43-9d83-25ec645e1f45"),
                        Description = "closed above the 9 EMA"
                    });
                }
                else if (item.Close < item.EMA9 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].EMA9)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("fd8161e4-10f9-48ba-9b5a-56be2f06be8c"),
                        Description = "closed below the 9 EMA"
                    });
                }

            }

            return events;
        }

        //Handle EMA21 Events.
        public static List<Event> GetEma21Events(List<RichDailyStockData> data)
        {
            data = data.Where(d => d.EMA21 != 0).OrderBy(d => d.Date).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.EMA21 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].EMA21)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("4d69d536-6ff1-449f-9516-e20120ed543e"),
                        Description = "closed above the 21 EMA"
                    });
                }
                else if (item.Close < item.EMA21 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].EMA21)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("ac889256-09c1-467e-9b9a-488d9288e0ae"),
                        Description = "closed below the 21 EMA"
                    });
                }

            }

            return events;
        }

        //Handle EMA8 Events.
        public static List<Event> GetEma8Events(List<RichDailyStockData> data)
        {
            data = data.Where(d => d.EMA8 != 0).OrderBy(d => d.Date).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.EMA8 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].EMA8)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("d0f5e50c-92e8-45c2-8a79-8f90710d4357"),
                        Description = "closed above the 8 EMA"
                    });
                }
                else if (item.Close < item.EMA8 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].EMA8)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("7926410f-3105-4077-bf53-be9c41a16698"),
                        Description = "closed below the 8 EMA"
                    });
                }

            }

            return events;
        }

        //Handle EMA5 Events.
        public static List<Event> GetEma5Events(List<RichDailyStockData> data)
        {
            data = data.Where(d => d.EMA5 != 0).OrderBy(d => d.Date).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.EMA5 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].EMA5)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("fde6ee34-b5e2-4872-8bad-24fd53fe1ab0"),
                        Description = "closed above the 5 EMA"
                    });
                }
                else if (item.Close < item.EMA5 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].EMA5)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("d4870c2a-5b45-4059-8ce2-6a878a44befd"),
                        Description = "closed below the 5 EMA"
                    });
                }

            }

            return events;
        }

        //Handle EMA10 Events.
        public static List<Event> GetEma10Events(List<RichDailyStockData> data)
        {
            data = data.Where(d => d.EMA10 != 0).OrderBy(d => d.Date).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                if (item.Close > item.EMA10 && data[data.IndexOf(item) - 1].Close < data[data.IndexOf(item) - 1].EMA10)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("29f3b4ad-eb76-442d-bcc0-e637d2e86b2e"),
                        Description = "closed above the 10 EMA"
                    });
                }
                else if (item.Close < item.EMA10 && data[data.IndexOf(item) - 1].Close > data[data.IndexOf(item) - 1].EMA10)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("53381fc3-874d-48fa-be3e-2ccf9b59a98f"),
                        Description = "closed below the 10 EMA"
                    });
                }

            }

            return events;
        }

        //Handle EMA9 Cross EMA21 Events.
        public static List<Event> GetEma9CrossEma21Events(List<RichDailyStockData> data)
        {
            data = data.Where(d => d.EMA9 != 0 && d.EMA21 != 0).OrderBy(d => d.Date).ToList();

            var events = new List<Event>();

            foreach (var item in data)
            {
                if (data.IndexOf(item) < 2)
                {
                    continue;
                }

                if (data[data.IndexOf(item) - 1].EMA9 < data[data.IndexOf(item) - 1].EMA21 && data[data.IndexOf(item) - 2].EMA9 > data[data.IndexOf(item) - 2].EMA21 && item.EMA9 > item.EMA21)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bullish,
                        EventTypeId = Guid.Parse("6ca2de88-bfec-460a-ab25-d2d2e4964c40"),
                        Description = "9 EMA crossed above 21 EMA"
                    });
                }
                else if (data[data.IndexOf(item) - 1].EMA9 > data[data.IndexOf(item) - 1].EMA21 && data[data.IndexOf(item) - 2].EMA9 < data[data.IndexOf(item) - 2].EMA21 && item.EMA9 < item.EMA21)
                {
                    events.Add(new Event
                    {
                        Symbol = item.Symbol,
                        Date = item.Date,
                        Direction = Direction.Bearish,
                        EventTypeId = Guid.Parse("618ac9a9-4527-4aaf-8be4-430728ac0826"),
                        Description = "9 EMA crossed below 21 EMA"
                    });
                }

            }

            return events;
        }
    }
}
