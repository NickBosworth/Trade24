namespace Trade24.Service.Detection.Plugins.SMA.Price
{
    using Trade24.Service.Data;
    public class BullishPriceCrossSMA50Plugin : BasePriceCrossSMAPlugin, IDetector
    {
        public BullishPriceCrossSMA50Plugin()
        {
            Id = Guid.Parse("f7ca59a1-e269-4f9a-bb67-cfe75bf224fb");
            Name = "Bullish Price Cross 50 SMA";
            Description = "Bullish Price Cross SMA50";
            MinimumDays = 50;
            LastDayIsMatch = true;
            SignalType = SignalType.Bullish;
            Length = 50;
            BullishCheck = true;

        }
    }
}
