using Trade24.Service.Collection;
using Trade24.Service.Data;

namespace Trade24.Service.Jobs
{
    public static class UpdateHistory
    {
        public static void Update()
        {


            //Get all stock tickers from the database where HasHistory is false.
            using (var db = new SqliteContext())
            {
                var stockTickersWithoutHistory = db.StockTickers.Where(st => !st.HasHistory && !st.IsErroneous).ToList();

                //For each item get the stock prices starting with the last years worth and working backwards until a successful empty data set is returned.
                foreach (var stockTicker in stockTickersWithoutHistory)
                {
                    Console.WriteLine($"Getting stock prices for {stockTicker.Symbol}...");

                    var startDate = DateTime.Now.AddMonths(-6);
                    var endDate = DateTime.Now;

                    while (true)
                    {
                        //Annouce what is being done.
                        Console.WriteLine($"Getting stock prices for {stockTicker.Symbol} between {startDate} and {endDate}...");

                        StockResponse stockResponse = new StockResponse { Status = ProcessStatus.NotAuthorized };

                        while (stockResponse.Status == ProcessStatus.NotAuthorized)
                        {
                            stockResponse = YahooDailyPriceCollector.GetStockPrices(stockTicker.Symbol, startDate, endDate);
                            if (stockResponse.Status == ProcessStatus.NotAuthorized)
                            {
                                Console.WriteLine($"Failed to collect stock prices for {stockTicker.Symbol}. Error: {stockResponse.Status}");
                                Console.WriteLine("Sleeping for 1 minute...");
                                Thread.Sleep(60000);
                                Console.WriteLine($"Retrying to collect stock prices for {stockTicker.Symbol}...");
                            }

                        }



                        if (stockResponse.Status == ProcessStatus.Success)
                        {


                            //If the response is successful and there are prices then add the prices to the database and set the start date to the last date in the response plus 1 day.
                            foreach (var price in stockResponse.Prices.GroupBy(price => price.Date).Select(group => group.First()))
                            {
                                //Add or update the daily stock data.
                                var existingDailyStockData = db.DailyStockData.FirstOrDefault(ds => ds.Symbol == stockTicker.Symbol && ds.Date == price.Date);
                                if (existingDailyStockData != null)
                                {
                                    existingDailyStockData.Symbol = stockTicker.Symbol;
                                    existingDailyStockData.Open = price.Open;
                                    existingDailyStockData.High = price.High;
                                    existingDailyStockData.Low = price.Low;
                                    existingDailyStockData.Close = price.Close;
                                    existingDailyStockData.Volume = price.Volume;
                                }
                                else
                                {
                                    db.DailyStockData.Add(new DailyStockData { Symbol = stockTicker.Symbol, Date = price.Date, Open = price.Open, High = price.High, Low = price.Low, Close = price.Close, Volume = price.Volume });
                                }


                            }

                            db.SaveChanges();
                            endDate = stockResponse.Prices.OrderBy(p => p.Date).First().Date.AddDays(-1);
                            startDate = endDate.AddYears(-1);

                        }
                        else
                        {
                            stockTicker.HasHistory = true;
                            db.SaveChanges();
                            Console.WriteLine($"Successfully collected stock prices for {stockTicker.Symbol}.");
                            break;
                        }
                    }
                }
            }


        }

    }
}
