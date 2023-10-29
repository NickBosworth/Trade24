//using Trade24.Service.Collection;
//using Trade24.Service.Data;

//namespace Trade24.Service.Jobs
//{
//    public static class UpdateSymbols
//    {
//        public static void Update()
//        {
//            var yahooMostActiveSymbolCollector = new YahooMostActiveSymbolCollector();

//            //Update known stock tickers
//            Console.WriteLine("Updating known stock tickers...");
//            var symbolsResponse = yahooMostActiveSymbolCollector.CollectSymbols();

//            if (symbolsResponse.Success)
//            {
//                Console.WriteLine($"Collected {symbolsResponse.Symbols.Count} symbols from Yahoo Finance.");

//                //For each symbol, try and get the existing one from the database. If it exists do nothing but if it does not exist create a new StockTicker object with the item as the ticker symbol value.
//                foreach (var symbol in symbolsResponse.Symbols)
//                {
//                    using (var db = new SqliteContext())
//                    {
//                        var existingStockTicker = db.StockTickers.FirstOrDefault(st => st.Symbol == symbol);

//                        if (existingStockTicker == null)
//                        {
//                            db.StockTickers.Add(new StockTicker { Symbol = symbol });
//                            db.SaveChanges();
//                            Console.WriteLine($"Added new stock ticker: {symbol}");
//                        }
//                    }
//                }
//            }
//            else
//            {
//                Console.WriteLine($"Failed to collect symbols from Yahoo Finance. Error: {symbolsResponse.ErrorMessage}");
//            }


//        }

//    }
//}
