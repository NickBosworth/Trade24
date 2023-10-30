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

                        try
                        {

                            plugin.Detect(data);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error running plugin {plugin.Name} for {symbol.Symbol} for {dayToProcess.Date}");
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            continue;
                        }

                        if (plugin.LastDayIsMatch)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Last day is match for {plugin.Name} for {symbol.Symbol}");
                            Console.ResetColor();

                            var pluginEvent = new PluginEvent()
                            {
                                PluginId = plugin.Id,
                                Symbol = symbol.Symbol,
                                Date = dayToProcess.Date,
                                SignalType = plugin.SignalType,
                                Description = plugin.Description,
                                Name = plugin.Name,
                            };

                            //check if we have a plugin event for this symbol, date and plugin.
                            var existingPluginEvent = context.PluginEvents.FirstOrDefault(x => x.Symbol == symbol.Symbol && x.Date == dayToProcess.Date && x.PluginId == plugin.Id);

                            if (existingPluginEvent != null)
                            {
                                //update the existing plugin event
                                existingPluginEvent.Description = pluginEvent.Description;
                                existingPluginEvent.Name = pluginEvent.Name;
                                existingPluginEvent.SignalType = pluginEvent.SignalType;
                                existingPluginEvent.Symbol = pluginEvent.Symbol;
                                existingPluginEvent.Date = pluginEvent.Date;
                                existingPluginEvent.PluginId = pluginEvent.PluginId;
                            }
                            else
                            {
                                //Add it.
                                context.PluginEvents.Add(pluginEvent);
                            }

                            context.SaveChanges();
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
