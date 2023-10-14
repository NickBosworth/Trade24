namespace Trade24.DataCollection
{
    public class StockResponse
    {
        public ProcessStatus Status { get; set; }
        public List<StockPrice> Prices { get; set; } = new List<StockPrice>();
    }
}
