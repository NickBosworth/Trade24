namespace Trade24.Service.Collection
{
    public class YahooDailyPriceCollector
    {
        private const string BASE_URL = "https://query1.finance.yahoo.com/v7/finance/download/{0}?period1={1}&period2={2}&interval=1d&events=history&includeAdjustedClose=true";

        public static StockResponse GetStockPrices(string ticker, DateTime startDate, DateTime endDate)
        {
            StockResponse response = new StockResponse();
            try
            {
                // Ensure the dates are set to the start of the day
                startDate = startDate.Date;
                endDate = endDate.Date;

                // Convert the dates to UNIX timestamps
                long startTimestamp = (long)(startDate - new DateTime(1970, 1, 1)).TotalSeconds;
                long endTimestamp = (long)(endDate - new DateTime(1970, 1, 1)).TotalSeconds;

                using HttpClient client = new HttpClient();
                HttpResponseMessage httpResponse = client.GetAsync(string.Format(BASE_URL, ticker, startTimestamp, endTimestamp)).Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    // Assume CSV format is: Date,Open,High,Low,Close,AdjustedClose,Volume
                    string[] dataLines = httpResponse.Content.ReadAsStringAsync().Result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                    // Skip the header line
                    for (int i = 1; i < dataLines.Length; i++)
                    {
                        string line = dataLines[i];
                        string[] parts = line.Split(',');
                        if (parts.Length == 7)
                        {
                            StockPrice stockPrice = new StockPrice
                            {
                                Date = DateTime.Parse(parts[0]),
                                Open = decimal.Parse(parts[1]),
                                High = decimal.Parse(parts[2]),
                                Low = decimal.Parse(parts[3]),
                                Close = decimal.Parse(parts[4]),
                                AdjustedClose = decimal.Parse(parts[5]),
                                Volume = int.Parse(parts[6])
                            };
                            response.Prices.Add(stockPrice);
                        }
                    }

                    response.Status = ProcessStatus.Success;
                }
                else if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    response.Status = ProcessStatus.NotAuthorized;
                }
                else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    response.Status = ProcessStatus.NotFound;
                }
                else
                {
                    response.Status = ProcessStatus.HttpError;
                }
            }
            catch
            {
                response.Status = ProcessStatus.InternalError;
            }

            return response;
        }
    }
}
