using Trade24.Service.Data;

namespace Trade24.Service.Detection.Plugins
{
    public abstract class BasePlugin : IDetector
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinimumDays { get; set; }
        public bool LastDayIsMatch { get; set; }
        public SignalType SignalType { get; set; }

        public virtual void Detect(List<DailyStockData> data)
        {
            throw new Exception("Not implemented");
        }

    }
}
