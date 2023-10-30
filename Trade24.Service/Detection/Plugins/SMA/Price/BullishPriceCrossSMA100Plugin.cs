namespace Trade24.Service.Detection.Plugins.SMA.Price
{
    using Trade24.Service.Data;
    public class BullishPriceCrossSMA100Plugin : BasePriceCrossSMAPlugin, IDetector
    {
        public BullishPriceCrossSMA100Plugin()
        {
            Id = Guid.Parse("3f286183-f9fc-4831-97c2-9e85ae183982");
            Name = "Bullish Price Cross 100 SMA";
            Description = "Bullish Price Cross SMA100";
            MinimumDays = 100;
            LastDayIsMatch = true;
            SignalType = SignalType.Bullish;
            Length = 100;
            BullishCheck = true;
        }
    }
}
