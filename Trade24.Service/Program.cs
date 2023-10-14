using Microsoft.EntityFrameworkCore;
using Trade24.Common.Data;
using Trade24.DataCollection;


Console.WriteLine("Started the Trade24 Service...");

//Initialize the database
using (var db = new Context())
{
    Console.WriteLine("Ensuring the database is created...");
    db.Database.EnsureCreated();
    //Ensure the database is updated to the latest model.
    Console.WriteLine("Ensuring the database is updated to the latest model...");
    //if there are pending migrations perform them.
    if (db.Database.GetPendingMigrations().Any())
    {
        Console.WriteLine("Applying pending migrations...");
        db.Database.Migrate();
    }
    Console.WriteLine("Database is ready.");
}

//Create a run record for each type if one does not exist. The default value for LastRun is DateTime.MinValue.
using (var db = new Context())
{
    foreach (RunRecordType type in Enum.GetValues(typeof(RunRecordType)))
    {
        var existingRunRecord = db.RunRecords.FirstOrDefault(rr => rr.Type == type);

        if (existingRunRecord == null)
        {
            db.RunRecords.Add(new RunRecord { Type = type });
            db.SaveChanges();
        }
    }
}

//Create a symbol collector for use.
var yahooMostActiveSymbolCollector = new YahooMostActiveSymbolCollector();

while (true)
{
    //Get the last run dates for each type.
    DateTime lastUpdateTickersRun;
    DateTime lastGetHistoryRun;
    DateTime lastUpdatePriceDataRun;

    using (var db = new Context())
    {
        lastUpdateTickersRun = db.RunRecords.FirstOrDefault(rr => rr.Type == RunRecordType.UpdateTickers).LastRun;
        lastGetHistoryRun = db.RunRecords.FirstOrDefault(rr => rr.Type == RunRecordType.GetHistory).LastRun;
        lastUpdatePriceDataRun = db.RunRecords.FirstOrDefault(rr => rr.Type == RunRecordType.UpdatePriceData).LastRun;

    }

    //determin how many hours ago each type was last run.
    var hoursSinceLastUpdateTickersRun = (DateTime.Now - lastUpdateTickersRun).TotalHours;
    var hoursSinceLastGetHistoryRun = (DateTime.Now - lastGetHistoryRun).TotalHours;
    var hoursSinceLastUpdatePriceDataRun = (DateTime.Now - lastUpdatePriceDataRun).TotalHours;


    if (hoursSinceLastUpdateTickersRun >= 24)
    {

        //Update known stock tickers
        Console.WriteLine("Updating known stock tickers...");
        var symbolsResponse = yahooMostActiveSymbolCollector.CollectSymbols();

        if (symbolsResponse.Success)
        {
            Console.WriteLine($"Collected {symbolsResponse.Symbols.Count} symbols from Yahoo Finance.");

            //For each symbol, try and get the existing one from the database. If it exists do nothing but if it does not exist create a new StockTicker object with the item as the ticker symbol value.
            foreach (var symbol in symbolsResponse.Symbols)
            {
                using (var db = new Context())
                {
                    var existingStockTicker = db.StockTickers.FirstOrDefault(st => st.Symbol == symbol);

                    if (existingStockTicker == null)
                    {
                        db.StockTickers.Add(new StockTicker { Symbol = symbol });
                        db.SaveChanges();
                        Console.WriteLine($"Added new stock ticker: {symbol}");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine($"Failed to collect symbols from Yahoo Finance. Error: {symbolsResponse.ErrorMessage}");
        }

        //Update the last run date for UpdateTickers.
        using (var db = new Context())
        {
            var runRecord = db.RunRecords.FirstOrDefault(rr => rr.Type == RunRecordType.UpdateTickers);
            runRecord.LastRun = DateTime.Now;
            db.SaveChanges();
        }
    }

    if (hoursSinceLastGetHistoryRun > 24)
    {

        //Get all stock tickers from the database where HasHistory is false.
        using (var db = new Context())
        {
            var stockTickersWithoutHistory = db.StockTickers.Where(st => !st.HasHistory && !st.IsErroneous).ToList();

            //For each item get the stock prices starting with the last years worth and working backwards until a successful empty data set is returned.
            foreach (var stockTicker in stockTickersWithoutHistory)
            {
                Console.WriteLine($"Getting stock prices for {stockTicker.Symbol}...");

                var startDate = DateTime.Now.AddYears(-1);
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

        //Update the last run date for GetHistory.
        using (var db = new Context())
        {
            var runRecord = db.RunRecords.FirstOrDefault(rr => rr.Type == RunRecordType.GetHistory);
            runRecord.LastRun = DateTime.Now;
            db.SaveChanges();
        }
    }

    if (hoursSinceLastUpdatePriceDataRun > 1)
    {
        //For each stock ticker fetch the last 30 days worth of data and insert or update it into the database.
        using (var db = new Context())
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

    //Announce sleeping for 1 minute and speed.
    Console.WriteLine("Sleeping for 1 minute...");
    Thread.Sleep(60000);
}