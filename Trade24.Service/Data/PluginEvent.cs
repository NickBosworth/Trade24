using Microsoft.EntityFrameworkCore;

namespace Trade24.Service.Data
{
    [PrimaryKey(nameof(PluginId), nameof(Symbol), nameof(Date))]
    public class PluginEvent
    {
        public string MarketCode { get; set; }
        public Guid PluginId { get; set; }
        public DateTime Date { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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
