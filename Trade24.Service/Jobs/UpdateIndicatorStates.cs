//using Microsoft.CodeAnalysis;
//using Trade24.Service.Data;
//using Trade24.Service.Detection;

//namespace Trade24.Service.Jobs
//{
//    public static class UpdateIndicatorStates
//    {
//        public static void Update()
//        {
//            var jobNumber = 0;
//            var jobCount = 0;

//            //Get all of the stock tickets where HasHistory is true.
//            using (var db = new SqliteContext())
//            {
//                var stockTickersWithHistory = db.StockTickers.Where(st => st.HasHistory && !st.IsErroneous).OrderBy(st => st.Symbol).ToList();
//                jobCount = stockTickersWithHistory.Count;
//                foreach (var stock in stockTickersWithHistory)
//                {
//                    jobNumber++;

//                    // Using the external db variable for SqliteContext
//                    // Assuming the db variable is defined somewhere above in your code.

//                    // Write the percentage of the way through we are to the console.
//                    var percentage = (decimal)jobNumber / (decimal)jobCount;
//                    Console.WriteLine($"Updating indicator states for {stock.Symbol}... {percentage.ToString("P2")}");

//                    // Get all of the daily stock data for the stock.
//                    var dailyStockData = db.DailyStockData.Where(ds => ds.Symbol == stock.Symbol).OrderBy(ds => ds.Date).ToList();

//                    // Enrich the data.
//                    var richData = EnrichmentService.Enrich(dailyStockData);

//                    // Ensure there are bullish, neutral and bearish states for the stock for each day in the rich data.
//                    var states = db.DailyStockIndicatorStates.Where(ds => ds.Symbol == stock.Symbol).ToList();

//                    foreach (var item in richData)
//                    {
//                        if (!states.Any(bs => bs.Date == item.Date))
//                        {
//                            var newState = new DailyStockIndicatorState() { Symbol = item.Symbol, Date = item.Date };
//                            states.Add(newState);
//                            db.DailyStockIndicatorStates.Add(newState);
//                        }
//                    }

//                    // Update or insert the states.
//                    StateDetector.UpdateState(richData, states);

//                    // Save the changes.
//                    db.SaveChanges();
//                }



//            }
//        }
//    }
//}
