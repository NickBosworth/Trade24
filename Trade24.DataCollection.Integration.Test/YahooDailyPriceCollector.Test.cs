namespace Trade24.DataCollection.Integration.Test
{
    public class YahooDailyPriceCollectorTest
    {
        [Fact]
        public void Test_GetStockPrices_For_August2023_THG_L()
        {
            // Arrange
            string ticker = "THG.L";
            DateTime startDate = new DateTime(2023, 8, 1);
            DateTime endDate = new DateTime(2023, 8, 31);

            // Act       
            StockResponse result = YahooDailyPriceCollector.GetStockPrices(ticker, startDate, endDate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ProcessStatus.Success, result.Status);
            Assert.NotEmpty(result.Prices);

            // Additional checks can be made based on specific requirements, for example:
            // Ensure all dates in the response are within August 2023
            foreach (var price in result.Prices)
            {
                Assert.True(price.Date >= startDate && price.Date <= endDate);
            }
        }


        [Fact]
        public void Test_CollectSymbols_ReturnsValidSymbols()
        {
            // Arrange
            var collector = new YahooMostActiveSymbolCollector();

            // Act
            var result = collector.CollectSymbols();

            // Assert
            Assert.True(result.Success, result.ErrorMessage);
            Assert.NotNull(result.Symbols);
            Assert.NotEmpty(result.Symbols);

            // Depending on what further validation you need, you could add more assertions.
            // For example, ensure symbols match a specific pattern, etc.
            foreach (var symbol in result.Symbols)
            {
                Assert.False(string.IsNullOrWhiteSpace(symbol));
                // You could further check if symbols have a ".L" suffix for LSE stocks, or any other specific patterns.
            }
        }
    }
}