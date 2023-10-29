using Trade24.Service.Collection;
using Trade24.Service.Data;

namespace Trade24.Service.Jobs
{
    public static class UpdateCurrent
    {
        public static void Update()
        {

            //For each stock ticker fetch the last 30 days worth of data and insert or update it into the database.
            using (var db = new SqliteContext())
            {
                var stockTickers = db.StockTickers.ToList();

                foreach (var stockTicker in stockTickers)
                {
                    Console.WriteLine($"Getting stock prices for {stockTicker.Symbol}...");

                    var startDate = DateTime.Now.AddDays(-30);
                    var endDate = DateTime.Now;

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

                        //If the response was an error other than not authorised, mark the symbol as erroneous and continue to the next record.
                        if (stockResponse.Status != ProcessStatus.NotAuthorized && stockResponse.Status != ProcessStatus.Success)
                        {
                            stockTicker.IsErroneous = true;
                            db.SaveChanges();
                            Console.WriteLine($"Failed to collect stock prices for {stockTicker.Symbol}. Error: {stockResponse.Status}");
                            break;
                        }
                    }

                    if (stockResponse.Status == ProcessStatus.Success)
                    {
                        //Insert or update the daily stock data.
                        foreach (var price in stockResponse.Prices.GroupBy(price => price.Date).Select(group => group.First()))
                        {
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


                    }
                }

            }
        }
    }
}
