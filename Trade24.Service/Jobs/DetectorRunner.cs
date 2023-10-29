using Trade24.Service.Data;
using Trade24.Service.Detection.Plugins;

namespace Trade24.Service.Jobs
{
    public static class DetectorRunner
    {
        public static void RunFor(DateTime dayToProcess)
        {
            Console.WriteLine("DetectorRunner started");

            var plugins = PluginLoader.LoadPlugins();

            Console.WriteLine($"Loaded {plugins.Count} plugins");

            using (var context = new SqliteContext())
            {
                var symbols = context.StockTickers.ToList();

                foreach (var symbol in symbols)
                {
                    var data = context.DailyStockData.Where(x => x.Symbol == symbol.Symbol && x.Date <= dayToProcess).OrderBy(x => x.Date).ToList();

                    if (!data.Any())
                    {
                        continue;
                    }

                    if (data.Any() && data.Last().Date.Date < dayToProcess.Date.Date)
                    {
                        continue;
                    }

                    foreach (var plugin in plugins)
                    {
                        if (data.Count < plugin.MinimumDays)
                        {
                            continue;
                        }

                        plugin.Detect(data);

                        if (plugin.LastDayIsMatch)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Last day is match for {plugin.Name} for {symbol.Symbol}");
                            Console.ResetColor();
                        }
                    }
                }
            }
        }

        public static void RunBetween(DateTime startDate, DateTime endDate)
        {
            for (var day = startDate; day <= endDate; day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                Console.WriteLine($"Running for {day}");
                RunFor(day);
            }
        }
    }
}
