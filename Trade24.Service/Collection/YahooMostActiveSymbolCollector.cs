using HtmlAgilityPack;
using System.Net;

namespace Trade24.Service.Collection
{
    public class YahooMostActiveSymbolCollector
    {
        private const string BASE_URL = "https://uk.finance.yahoo.com/most-active?offset={0}&count=100";

        public SymbolCollectionResponse CollectSymbols()
        {
            var response = new SymbolCollectionResponse();

            try
            {
                int offset = 0;
                int? totalResults = null;

                while (true)
                {
                    var currentUrl = string.Format(BASE_URL, offset);
                    var currentPageSymbols = FetchSymbolsFromPage(currentUrl, out int startingNumberOfCurrentPage);

                    // Initialize the totalResults on the first iteration
                    if (totalResults == null)
                    {
                        totalResults = ParseTotalResults(currentUrl);
                    }

                    if (currentPageSymbols == null || currentPageSymbols.Count == 0 || totalResults.HasValue && startingNumberOfCurrentPage > totalResults.Value)
                    {
                        break; // End the loop if there are no symbols on the current page or the starting number exceeds total results.
                    }

                    response.Symbols.AddRange(currentPageSymbols);
                    offset += 100;
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            response.Symbols = response.Symbols.Distinct().ToList();
            return response;
        }

        private int? ParseTotalResults(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var htmlContent = httpClient.GetStringAsync(url).Result;
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                // Assuming the results are contained in a <span> (you'll need to adjust the XPath based on the actual structure of the page)
                var resultNode = htmlDocument.DocumentNode.SelectSingleNode("//span[contains(text(), 'of') and contains(text(), 'results')]");

                if (resultNode != null)
                {
                    var parts = resultNode.InnerText.Split(' ');
                    if (parts.Length > 2)
                    {
                        if (int.TryParse(parts[parts.Length - 2], out int totalResults))
                        {
                            return totalResults;
                        }
                    }
                }
            }

            return null;
        }

        private List<string> FetchSymbolsFromPage(string url, out int startingNumberOfCurrentPage)
        {
            var symbols = new List<string>();
            startingNumberOfCurrentPage = 0; // Initialize to 0

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage httpResponse;

                try
                {
                    httpResponse = httpClient.GetAsync(url).Result;

                    if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Thread.Sleep(60000);  // Sleep for 60 seconds
                        httpResponse = httpClient.GetAsync(url).Result;  // Retry
                    }

                    httpResponse.EnsureSuccessStatusCode();

                    var htmlContent = httpResponse.Content.ReadAsStringAsync().Result;
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(htmlContent);

                    // Extract ticker symbols from the links with the given pattern
                    var tickerNodes = htmlDocument.DocumentNode.SelectNodes("//a[starts-with(@href, '/quote/') and contains(@href, '?p=')]");

                    foreach (var node in tickerNodes)
                    {
                        var hrefValue = node.GetAttributeValue("href", "");
                        var parts = hrefValue.Split('?');

                        if (parts.Length > 1)
                        {
                            var ticker = parts[0].Replace("/quote/", "").Trim();
                            symbols.Add(ticker);
                        }
                    }

                    // Extract the starting number of the current page
                    var resultNode = htmlDocument.DocumentNode.SelectSingleNode("//span[contains(text(), 'of') and contains(text(), 'results')]");
                    if (resultNode != null)
                    {
                        var parts = resultNode.InnerText.Split(' ');
                        if (parts.Length > 2)
                        {
                            var rangeParts = parts[0].Split('-');
                            if (rangeParts.Length > 0)
                            {
                                int.TryParse(rangeParts[0], out startingNumberOfCurrentPage);
                            }
                        }
                    }
                }
                catch
                {
                    // Here, you can handle or log the exception if needed.
                    // Returning null to indicate an error occurred during the page processing.
                    return null;
                }
            }

            return symbols;
        }

    }
}
