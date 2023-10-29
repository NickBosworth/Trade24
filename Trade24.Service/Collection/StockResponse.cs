namespace Trade24.Service.Collection
{
    public class StockResponse
    {
        public ProcessStatus Status { get; set; }
        public List<StockPrice> Prices { get; set; } = new List<StockPrice>();
    }
}
