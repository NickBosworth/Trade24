using Trade24.Service.Data;

namespace Trade24.Service.Detection
{
    public interface IDetector
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MinimumDays { get; set; }

        public void Detect(List<DailyStockData> data);

        public bool LastDayIsMatch { get; set; }

        public SignalType SignalType { get; set; }
    }

    public enum SignalType
    {
        Bullish,
        StrongBullish,
        Bearish,
        StrongBearish
    }
}
